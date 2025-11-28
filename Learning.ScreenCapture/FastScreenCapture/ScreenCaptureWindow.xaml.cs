using FastScreenCapture.Controls;
using FastScreenCapture.Helpers;
using FastScreenCapture.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FastScreenCapture
{
	/// <summary>
	/// 截图工具主窗口
	/// </summary>
	public partial class ScreenCaptureWindow : Window
	{
		#region Fields

		/// <summary>
		/// 屏幕宽度
		/// </summary>
		public static double ScreenWidth = SystemParameters.PrimaryScreenWidth;
		/// <summary>
		/// 屏幕高度
		/// </summary>
		public static double ScreenHeight = SystemParameters.PrimaryScreenHeight;
		/// <summary>
		/// 屏幕缩放比
		/// </summary>
		public static double ScreenScale = 1;
		/// <summary>
		/// 画图注册名称集合
		/// </summary>
		public List<NameAndLimit> list = new List<NameAndLimit>();
		/// <summary>
		/// 画图注册名称计数器
		/// </summary>
		public int num = 1;
		/// <summary>
		/// 截图区域是否开始首次圈定
		/// </summary>
		private bool _IsMouseDown = false;
		/// <summary>
		/// 截图区域是否首次圈定完毕
		/// </summary>
		private bool _IsCapture = false;
		/// <summary>
		/// 截图区域初始位置X轴坐标
		/// </summary>
		private double _X0 = 0;
		/// <summary>
		/// 截图区域初始位置Y轴坐标
		/// </summary>
		private double _Y0 = 0;
		/// <summary>
		/// 截图区域最小宽高值
		/// </summary>
		public static int MinSize = 10;

		#endregion

		#region Constructor

		public ScreenCaptureWindow()
		{
			_Current = this;
			InitializeComponent();
			DataContext = new AppModel();
			Background = new ImageBrush(ImageHelper.GetFullBitmapSource());
			WpfHelper.MainDispatcher = Dispatcher;
			MaxWindow();
			MaskLeft.Height = ScreenHeight;
			MaskRight.Height = ScreenHeight;
			// 计算Windows缩放比例
			ScreenHelper.ResetScreenScale();
		}

		#endregion

		#region 属性 Current

		private static ScreenCaptureWindow _Current = null;
		public static ScreenCaptureWindow Current
		{
			get
			{
				return _Current;
			}
		}

		#endregion

		#region 全屏+置顶

		private void MaxWindow()
		{
			Left = 0;
			Top = 0;
			Width = ScreenWidth;
			Height = ScreenHeight;
			Activate();
		}

		#endregion

		#region 注册画图

		public static void Register(object control)
		{
			var name = $"Draw{_Current.num}";
			_Current.MainCanvas.RegisterName(name, control);
			_Current.list.Add(new NameAndLimit(name));
			_Current.num++;
		}

		#endregion

		#region 截图区域添加画图

		public static void AddControl(UIElement e)
		{
			_Current.MainCanvas.Children.Add(e);
		}

		#endregion

		#region 截图区域撤回画图
		public static void RemoveControl(UIElement e)
		{
			_Current.MainCanvas.Children.Remove(e);
		}
		#endregion

		#region 撤销

		public void OnRevoke()
		{
			if (list.Count > 0)
			{
				var name = list[list.Count - 1].Name;
				var obj = MainCanvas.FindName(name);
				if (obj != null)
				{
					MainCanvas.Children.Remove(obj as UIElement);
					MainCanvas.UnregisterName(name);
					list.RemoveAt(list.Count - 1);
					MainImage.Limit = list.Count == 0 ? new Limit() : list[list.Count - 1].Limit;
				}
			}
		}

		#endregion

		#region 保存

		public void OnSave()
		{
			// 截图存储路径
			// 截图默认存储在安装路径Screenshot文件夹下
			string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Screenshot");
			if (!Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				directoryInfo.Create();
			}

			var sfd = new Microsoft.Win32.SaveFileDialog
			{
				FileName = $"截图{DateTime.Now.ToString("yyyyMMddhhmmss")}",
				Filter = "png|*.png",
				AddExtension = true,
				RestoreDirectory = true,
				InitialDirectory = path
			};

			if (sfd.ShowDialog() == true)
			{
				Hidden();
				Thread t = new Thread(new ThreadStart(() =>
				{
					Thread.Sleep(200);
					WpfHelper.SafeRun(() =>
					{
						var source = GetCapture();
						if (source != null)
						{
							ImageHelper.SaveToPng(source, sfd.FileName);
						}
						Close();
					});
				}))
				{
					IsBackground = true
				};
				t.Start();
			}
		}

		#endregion

		#region 获取截图

		private BitmapSource GetCapture()
		{
			return ImageHelper.GetBitmapSource((int)AppModel.Current.MaskLeftWidth + 1, (int)AppModel.Current.MaskTopHeight + 1, (int)MainImage.ActualWidth - 2, (int)MainImage.ActualHeight - 2);
		}

		#endregion

		#region 退出截图

		public void OnCancel()
		{
			Close();
		}

		#endregion

		#region 完成截图

		public void OnOK()
		{
			Hidden();
			Thread t = new Thread(new ThreadStart(() =>
			{
				Thread.Sleep(50);
				WpfHelper.SafeRun(() =>
				{
					var source = GetCapture();
					if (source != null)
					{
						Clipboard.SetImage(source);
					}
					Close();
				});
			}))
			{
				IsBackground = true
			};
			t.Start();
		}

		#endregion

		#region 截图前隐藏窗口

		private void Hidden()
		{
			// 隐藏尺寸RGB框
			if (AppModel.Current.MaskTopHeight < 40)
			{
				SizeRGB.Visibility = Visibility.Collapsed;
			}
			var need = SizeColorBar.Current.Selected == Tool.Null ? 30 : 67;
			if (AppModel.Current.MaskBottomHeight < need 
				&& AppModel.Current.MaskTopHeight < need)
			{
				ImageEditBar.Current.Visibility = Visibility.Collapsed;
				SizeColorBar.Current.Visibility = Visibility.Collapsed;
			}
			MainImage.ZoomThumbVisibility = Visibility.Collapsed;
		}

		#endregion

		#region 鼠标及键盘事件

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (_IsCapture)
			{
				return;
			}
			// 获取光标当前位置
			var point = e.GetPosition(this);
			_X0 = point.X;
			_Y0 = point.Y;
			_IsMouseDown = true;
			Canvas.SetLeft(MainImage, _X0);
			Canvas.SetTop(MainImage, _Y0);
			AppModel.Current.MaskLeftWidth = _X0;
			AppModel.Current.MaskRightWidth = ScreenWidth - _X0;
			AppModel.Current.MaskTopHeight = _Y0;
			Show_Size.Visibility = Visibility.Visible;
		}

		private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (!_IsMouseDown || _IsCapture)
			{
				return;
			}
			_IsMouseDown = false;
			if (MainImage.Width >= MinSize 
				&& MainImage.Height >= MinSize)
			{
				_IsCapture = true;
				ImageEditBar.Current.Visibility = Visibility.Visible;
				ImageEditBar.Current.ResetCanvas();
				Cursor = Cursors.Arrow;
			}
		}

		/// <summary>
		/// 鼠标光标在主窗体上移动时触发
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_MouseMove(object sender, MouseEventArgs e)
		{
			// 获取光标当前位置
			var point = e.GetPosition(this);
			// 将光标位置转换为屏幕坐标
			var screenP = PointToScreen(point);
			AppModel.Current.ShowRGB = ImageHelper.GetImageRGB((int)screenP.X, (int)screenP.Y);

			if (_IsCapture)
			{
				return;
			}

			if (Show_RGB.Visibility == Visibility.Collapsed)
			{
				Show_RGB.Visibility = Visibility.Visible;
			}

			if (_IsMouseDown)
			{
				// 计算截图区域的宽高
				var w = point.X - _X0;
				var h = point.Y - _Y0;
				if (w < MinSize || h < MinSize)
				{
					return;
				}
				if (MainImage.Visibility == Visibility.Collapsed)
				{
					MainImage.Visibility = Visibility.Visible;
				}
				AppModel.Current.MaskRightWidth = ScreenWidth - point.X;
				AppModel.Current.MaskTopWidth = w;
				AppModel.Current.MaskBottomHeight = ScreenHeight - point.Y;
				AppModel.Current.ChangeShowSize();
				MainImage.Width = w;
				MainImage.Height = h;
			}
			else
			{
				// 设置尺寸和RGB区域的显示位置
				AppModel.Current.ShowSizeLeft = point.X;
				AppModel.Current.ShowSizeTop = ScreenHeight - point.Y < 30 ? point.Y - 30 : point.Y + 10;
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				Close();
			}
		}

		#endregion
	}
}
