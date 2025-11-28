using FastScreenCapture.Helpers;
using FastScreenCapture.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FastScreenCapture.Controls
{
	public class MainImage : Control
	{
		#region Fields

		/// <summary>
		/// 鼠标光标当前位置
		/// </summary>
		public Point point;
		/// <summary>
		/// 矩形
		/// </summary>
		private Rectangle m_Rectangle = null;
		/// <summary>
		/// 椭圆
		/// </summary>
		private Ellipse m_Ellipse = null;
		/// <summary>
		/// 文本框控件
		/// </summary>
		public TextBoxControl m_TextBoxControl = null;
		/// <summary>
		/// 箭头
		/// </summary>
		private Path m_Arrow = null;
		/// <summary>
		/// 画刷
		/// </summary>
		private Path m_Line = null;
		private List<Point> points = null;
		private StreamGeometry geometry = new StreamGeometry();

		#endregion

		#region 属性 Current

		private static MainImage _Current = null;
		public static MainImage Current
		{
			get
			{
				return _Current;
			}
			set
			{
				_Current = value;
			}
		}

		#endregion

		#region MoveCursor DependencyProperty

		public Cursor MoveCursor
		{
			get { return (Cursor)GetValue(MoveCursorProperty); }
			set { SetValue(MoveCursorProperty, value); }
		}
		public static readonly DependencyProperty MoveCursorProperty =
				DependencyProperty.Register(nameof(MoveCursor), typeof(Cursor), typeof(MainImage),
				new PropertyMetadata(Cursors.SizeAll, new PropertyChangedCallback(MainImage.OnMoveCursorPropertyChanged)));

		private static void OnMoveCursorPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			if (obj is MainImage)
			{
				(obj as MainImage).OnMoveCursorValueChanged();
			}
		}

		protected void OnMoveCursorValueChanged()
		{

		}

		#endregion

		#region Direction DependencyProperty

		public Direction Direction
		{
			get { return (Direction)GetValue(DirectionProperty); }
			set { SetValue(DirectionProperty, value); }
		}
		public static readonly DependencyProperty DirectionProperty =
				DependencyProperty.Register(nameof(Direction), typeof(Direction), typeof(MainImage),
				new PropertyMetadata(Direction.Null, new PropertyChangedCallback(MainImage.OnDirectionPropertyChanged)));

		private static void OnDirectionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			if (obj is MainImage)
			{
				(obj as MainImage).OnDirectionValueChanged();
			}
		}

		protected void OnDirectionValueChanged()
		{

		}

		#endregion

		#region Limit DependencyProperty

		public Limit Limit
		{
			get { return (Limit)GetValue(LimitProperty); }
			set { SetValue(LimitProperty, value); }
		}
		public static readonly DependencyProperty LimitProperty =
				DependencyProperty.Register(nameof(Limit), typeof(Limit), typeof(MainImage),
				new PropertyMetadata(null, new PropertyChangedCallback(MainImage.OnLimitPropertyChanged)));

		private static void OnLimitPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			if (obj is MainImage)
			{
				(obj as MainImage).OnLimitValueChanged();
			}
		}

		protected void OnLimitValueChanged()
		{

		}

		#endregion

		#region ZoomThumbVisibility DependencyProperty

		public Visibility ZoomThumbVisibility
		{
			get { return (Visibility)GetValue(ZoomThumbVisibilityProperty); }
			set { SetValue(ZoomThumbVisibilityProperty, value); }
		}
		public static readonly DependencyProperty ZoomThumbVisibilityProperty =
				DependencyProperty.Register(nameof(ZoomThumbVisibility), typeof(Visibility), typeof(MainImage),
				new PropertyMetadata(Visibility.Visible, new PropertyChangedCallback(MainImage.OnZoomThumbVisibilityPropertyChanged)));

		private static void OnZoomThumbVisibilityPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			if (obj is MainImage)
			{
				(obj as MainImage).OnZoomThumbVisibilityValueChanged();
			}
		}

		protected void OnZoomThumbVisibilityValueChanged()
		{

		}

		#endregion

		#region Constructor

		static MainImage()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MainImage), new FrameworkPropertyMetadata(typeof(MainImage)));
		}

		public MainImage()
		{
			_Current = this;
			AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStart));
			AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompleted));
			AddHandler(Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnDragDelta));
			AddHandler(MouseMoveEvent, new MouseEventHandler(OnMove));
			Limit = new Limit();
		}

		#endregion

		#region 开始滑动事件

		/// <summary>
		/// 开始拖动滑块时触发
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDragStart(object sender, DragStartedEventArgs e)
		{
			Direction = (e.OriginalSource as ZoomThumb).Direction;
			if (SizeColorBar.Current.Selected != Tool.Null)
			{
				point = Mouse.GetPosition(this);
				if (SizeColorBar.Current.Selected == Tool.Text)
				{
					DrawText();
				}
			}
		}

		#endregion

		#region 滑动中事件

		/// <summary>
		/// 滑块拖动过程中触发
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDragDelta(object sender, DragDeltaEventArgs e)
		{
			// 拖动的水平距离
			var X = e.HorizontalChange;
			// 拖动的垂直距离
			var Y = e.VerticalChange;

			switch (Direction)
			{
				case Direction.Null:
					break;
				case Direction.Move:
					if (SizeColorBar.Current.Selected == Tool.Null)
					{
						OnMove(X, Y);
					}
					else
					{
						switch (SizeColorBar.Current.Selected)
						{
							case Tool.Rectangle:
								DrawRectangle(X, Y);
								break;
							case Tool.Ellipse:
								DrawEllipse(X, Y);
								break;
							case Tool.Arrow:
								DrawArrow(X, Y);
								break;
							case Tool.Line:
								DrawLine(X, Y);
								break;
							case Tool.Text:
								break;
							default:
								break;
						}
					}
					break;
				default:
					var str = Direction.ToString();
					if (X != 0)
					{
						if (str.Contains("Left"))
						{
							Left(X);
						}
						if (str.Contains("Right"))
						{
							Right(X);
						}
					}
					if (Y != 0)
					{
						if (str.Contains("Top"))
						{
							Top(Y);
						}
						if (str.Contains("Bottom"))
						{
							Bottom(Y);
						}
					}
					AppModel.Current.ChangeShowSize();
					break;
			}
			ImageEditBar.Current.ResetCanvas();
			SizeColorBar.Current.ResetCanvas();
		}
		#endregion

		#region 滑动结束事件

		private void OnDragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (Direction == Direction.Move && SizeColorBar.Current.Selected != Tool.Null)
			{
				switch (SizeColorBar.Current.Selected)
				{
					case Tool.Rectangle:
						if (m_Rectangle != null)
						{
							ResetLimit(Canvas.GetLeft(m_Rectangle), Canvas.GetTop(m_Rectangle), Canvas.GetLeft(m_Rectangle) + m_Rectangle.Width, Canvas.GetTop(m_Rectangle) + m_Rectangle.Height);
							ScreenCaptureWindow.Register(m_Rectangle);
							m_Rectangle = null;
						}
						break;
					case Tool.Ellipse:
						if (m_Ellipse != null)
						{
							ResetLimit(Canvas.GetLeft(m_Ellipse), Canvas.GetTop(m_Ellipse), Canvas.GetLeft(m_Ellipse) + m_Ellipse.Width, Canvas.GetTop(m_Ellipse) + m_Ellipse.Height);
							ScreenCaptureWindow.Register(m_Ellipse);
							m_Ellipse = null;
						}
						break;
					case Tool.Arrow:
						if (m_Arrow != null)
						{
							geometry.Clear();
							ResetLimit(points.Min(p => p.X), points.Min(p => p.Y), points.Max(p => p.X), points.Max(p => p.Y));
							points = null;
							ScreenCaptureWindow.Register(m_Arrow);
							m_Arrow = null;
						}
						break;
					case Tool.Line:
						if (m_Line != null)
						{
							geometry.Clear();
							ResetLimit(points.Min(p => p.X), points.Min(p => p.Y), points.Max(p => p.X), points.Max(p => p.Y));
							points = null;
							ScreenCaptureWindow.Register(m_Line);
							m_Line = null;
						}
						break;
					case Tool.Text:
						break;
					default:
						break;
				}
			}
			Direction = Direction.Null;
		}

		#endregion

		#region 画矩形

		private void DrawRectangle(double X, double Y)
		{
			if (m_Rectangle == null)
			{
				m_Rectangle = new Rectangle()
				{
					Fill = new SolidColorBrush(Colors.Transparent),
					Stroke = RectangleTool.Current.LineBrush,
					StrokeThickness = RectangleTool.Current.LineThickness
				};
				Panel.SetZIndex(m_Rectangle, -1);
				ScreenCaptureWindow.AddControl(m_Rectangle);
			}
			if (X > 0)
			{
				Canvas.SetLeft(m_Rectangle, point.X + AppModel.Current.MaskLeftWidth);
				m_Rectangle.Width = X < Width - point.X ? X : Width - point.X;
			}
			else
			{
				Canvas.SetLeft(m_Rectangle, -X < point.X ? point.X + X + AppModel.Current.MaskLeftWidth : AppModel.Current.MaskLeftWidth);
				m_Rectangle.Width = -X < point.X ? -X : point.X;
			}
			if (Y > 0)
			{
				Canvas.SetTop(m_Rectangle, point.Y + AppModel.Current.MaskTopHeight);
				m_Rectangle.Height = Y < Height - point.Y ? Y : Height - point.Y;
			}
			else
			{
				Canvas.SetTop(m_Rectangle, -Y < point.Y ? point.Y + Y + AppModel.Current.MaskTopHeight : AppModel.Current.MaskTopHeight);
				m_Rectangle.Height = -Y < point.Y ? -Y : point.Y;
			}
		}

		#endregion

		#region 画椭圆

		private void DrawEllipse(double X, double Y)
		{
			if (m_Ellipse == null)
			{
				m_Ellipse = new Ellipse()
				{
					Fill = new SolidColorBrush(Colors.Transparent),
					Stroke = EllipseTool.Current.LineBrush,
					StrokeThickness = EllipseTool.Current.LineThickness
				};
				Panel.SetZIndex(m_Ellipse, -1);
				ScreenCaptureWindow.AddControl(m_Ellipse);
			}
			if (X > 0)
			{
				Canvas.SetLeft(m_Ellipse, point.X + AppModel.Current.MaskLeftWidth);
				m_Ellipse.Width = X < Width - point.X ? X : Width - point.X;
			}
			else
			{
				Canvas.SetLeft(m_Ellipse, -X < point.X ? point.X + X + AppModel.Current.MaskLeftWidth : AppModel.Current.MaskLeftWidth);
				m_Ellipse.Width = -X < point.X ? -X : point.X;
			}
			if (Y > 0)
			{
				Canvas.SetTop(m_Ellipse, point.Y + AppModel.Current.MaskTopHeight);
				m_Ellipse.Height = Y < Height - point.Y ? Y : Height - point.Y;
			}
			else
			{
				Canvas.SetTop(m_Ellipse, -Y < point.Y ? point.Y + Y + AppModel.Current.MaskTopHeight : AppModel.Current.MaskTopHeight);
				m_Ellipse.Height = -Y < point.Y ? -Y : point.Y;
			}
		}

		#endregion

		#region 画箭头

		private void DrawArrow(double X, double Y)
		{
			var screen = new Point(point.X + AppModel.Current.MaskLeftWidth, point.Y + AppModel.Current.MaskTopHeight);
			if (m_Arrow == null)
			{
				m_Arrow = new Path()
				{
					Fill = ArrowTool.Current.LineBrush,
					StrokeThickness = ArrowTool.Current.LineThickness
				};
				Panel.SetZIndex(m_Arrow, -1);
				ScreenCaptureWindow.AddControl(m_Arrow);
			}
			var point2 = new Point(screen.X + X, screen.Y + Y);
			point2.X = point2.X < AppModel.Current.MaskLeftWidth ? AppModel.Current.MaskLeftWidth : point2.X > AppModel.Current.MaskLeftWidth + Width ? AppModel.Current.MaskLeftWidth + Width : point2.X;
			point2.Y = point2.Y < AppModel.Current.MaskTopHeight ? AppModel.Current.MaskTopHeight : point2.Y > AppModel.Current.MaskTopHeight + Height ? AppModel.Current.MaskTopHeight + Height : point2.Y;
			points = ArrowTool.Current.CreateArrow(screen, point2);

			using (var ctx = geometry.Open())
			{
				for (int i = 0; i < points.Count; i++)
				{
					if (i == 0)
					{
						ctx.BeginFigure(points[0], true, false);
					}
					else
					{
						ctx.LineTo(points[i], true, true);
					}
				}
			}
			m_Arrow.Data = geometry.Clone();
		}

		#endregion

		#region 画刷

		private void DrawLine(double X, double Y)
		{
			var screen = new Point(point.X + AppModel.Current.MaskLeftWidth, point.Y + AppModel.Current.MaskTopHeight);
			if (m_Line == null)
			{
				m_Line = new Path()
				{
					Stroke = LineTool.Current.LineBrush,
					StrokeThickness = LineTool.Current.LineThickness
				};
				points = new List<Point>
				{
					screen
				};
				Panel.SetZIndex(m_Line, -1);
				ScreenCaptureWindow.AddControl(m_Line);
			}
			var point2 = new Point(screen.X + X, screen.Y + Y);
			point2.X = point2.X < AppModel.Current.MaskLeftWidth ? AppModel.Current.MaskLeftWidth : point2.X > AppModel.Current.MaskLeftWidth + Width ? AppModel.Current.MaskLeftWidth + Width : point2.X;
			point2.Y = point2.Y < AppModel.Current.MaskTopHeight ? AppModel.Current.MaskTopHeight : point2.Y > AppModel.Current.MaskTopHeight + Height ? AppModel.Current.MaskTopHeight + Height : point2.Y;
			points.Add(point2);
			using (var ctx = geometry.Open())
			{
				for (int i = 0; i < points.Count; i++)
				{
					if (i == 0)
					{
						ctx.BeginFigure(points[0], true, false);
					}
					else
					{
						ctx.LineTo(points[i], true, true);
					}
				}
			}
			m_Line.Data = geometry.Clone();
		}

		#endregion

		#region 添加输入框

		private void DrawText()
		{
			if (m_TextBoxControl != null)
			{
				Focus();
			}
			else
			{
				m_TextBoxControl = new TextBoxControl()
				{
					FontSize = TextTool.Current.FontSize,
					Foreground = TextTool.Current.LineBrush
				};
				// 控制输入框在截图区域内
				if (point.X > Width - 36)
				{
					point.X = Width - 36;
				}
				if (point.Y > Height - 22)
				{
					point.Y = Height - 22;
				}
				var screen = new Point(point.X + AppModel.Current.MaskLeftWidth, point.Y + AppModel.Current.MaskTopHeight);
				// 设置TextBoxControl控件Canvas附加属性Left、Top的值
				Canvas.SetLeft(m_TextBoxControl, screen.X);
				Canvas.SetTop(m_TextBoxControl, screen.Y);
				// 将TextBoxControl控件添加到截图区域Canvas内
				ScreenCaptureWindow.AddControl(m_TextBoxControl);
			}
		}

		#endregion

		#region 拖动截图区域

		private void OnMove(double X, double Y)
		{
			#region X轴移动

			if (X > 0)
			{
				var max = AppModel.Current.MaskRightWidth > Limit.Left - AppModel.Current.MaskLeftWidth ? Limit.Left - AppModel.Current.MaskLeftWidth : AppModel.Current.MaskRightWidth;
				if (X > max)
				{
					X = max;
				}
			}
			else
			{
				var max = AppModel.Current.MaskLeftWidth > AppModel.Current.MaskLeftWidth + Width - Limit.Right ? AppModel.Current.MaskLeftWidth + Width - Limit.Right : AppModel.Current.MaskLeftWidth;
				if (-X > max)
				{
					X = -max;
				}
			}
			if (X != 0)
			{
				AppModel.Current.MaskLeftWidth += X;
				AppModel.Current.MaskRightWidth -= X;
				Canvas.SetLeft(this, Canvas.GetLeft(this) + X);
			}

			#endregion

			#region Y轴移动

			if (Y > 0)
			{
				var max = AppModel.Current.MaskBottomHeight > Limit.Top - AppModel.Current.MaskTopHeight ? Limit.Top - AppModel.Current.MaskTopHeight : AppModel.Current.MaskBottomHeight;
				if (Y > max)
				{
					Y = max;
				}
			}
			else
			{
				var max = AppModel.Current.MaskTopHeight > AppModel.Current.MaskTopHeight + Height - Limit.Bottom ? AppModel.Current.MaskTopHeight + Height - Limit.Bottom : AppModel.Current.MaskTopHeight;
				if (-Y > max)
				{
					Y = -max;
				}
			}
			if (Y != 0)
			{
				AppModel.Current.MaskTopHeight += Y;
				AppModel.Current.MaskBottomHeight -= Y;
				Canvas.SetTop(this, Canvas.GetTop(this) + Y);
			}

			#endregion
		}

		#endregion

		#region 左缩放

		private void Left(double X)
		{
			if (X > 0)
			{
				var max = ScreenCaptureWindow.Current.list.Count == 0 ? Width - ScreenCaptureWindow.MinSize
					: Limit.Left - AppModel.Current.MaskLeftWidth < Width - ScreenCaptureWindow.MinSize ? Limit.Left - AppModel.Current.MaskLeftWidth
					: Width - ScreenCaptureWindow.MinSize;
				if (X > max)
				{
					X = max;
				}
			}
			else
			{
				var max = AppModel.Current.MaskLeftWidth;
				if (-X > max)
				{
					X = -max;
				}
			}
			if (X != 0)
			{
				Width -= X;
				Canvas.SetLeft(this, Canvas.GetLeft(this) + X);
				AppModel.Current.MaskLeftWidth += X;
				AppModel.Current.MaskTopWidth -= X;
			}
		}

		#endregion

		#region 右缩放

		private void Right(double X)
		{
			if (X > 0)
			{
				var max = AppModel.Current.MaskRightWidth;
				if (X > max)
				{
					X = max;
				}
			}
			else
			{
				var max = ScreenCaptureWindow.Current.list.Count == 0 ? Width - ScreenCaptureWindow.MinSize
					: AppModel.Current.MaskLeftWidth + Width - Limit.Right < Width - ScreenCaptureWindow.MinSize ? AppModel.Current.MaskLeftWidth + Width - Limit.Right
					: Width - ScreenCaptureWindow.MinSize;
				if (-X > max)
				{
					X = -max;
				}
			}
			if (X != 0)
			{
				Width += X;
				AppModel.Current.MaskRightWidth -= X;
				AppModel.Current.MaskTopWidth += X;
			}
		}

		#endregion

		#region 上缩放

		private void Top(double Y)
		{
			if (Y > 0)
			{
				var max = ScreenCaptureWindow.Current.list.Count == 0 ? Height - ScreenCaptureWindow.MinSize
					: Limit.Top - AppModel.Current.MaskTopHeight < Height - ScreenCaptureWindow.MinSize ? Limit.Top - AppModel.Current.MaskTopHeight
					: Height - ScreenCaptureWindow.MinSize;
				if (Y > max)
				{
					Y = max;
				}
			}
			else
			{
				var max = AppModel.Current.MaskLeftWidth;
				if (-Y > max)
				{
					Y = -max;
				}
			}
			if (Y != 0)
			{
				Height -= Y;
				Canvas.SetTop(this, Canvas.GetTop(this) + Y);
				AppModel.Current.MaskTopHeight += Y;
			}
		}

		#endregion

		#region 下缩放

		private void Bottom(double Y)
		{
			if (Y > 0)
			{
				var max = AppModel.Current.MaskBottomHeight;
				if (Y > max)
				{
					Y = max;
				}
			}
			else
			{
				var max = ScreenCaptureWindow.Current.list.Count == 0 ? Height - ScreenCaptureWindow.MinSize
					: AppModel.Current.MaskTopHeight + Height - Limit.Bottom < Height - ScreenCaptureWindow.MinSize ? AppModel.Current.MaskTopHeight + Height - Limit.Bottom
					: Height - ScreenCaptureWindow.MinSize;
				if (-Y > max)
				{
					Y = -max;
				}
			}
			if (Y != 0)
			{
				Height += Y;
				AppModel.Current.MaskBottomHeight -= Y;
			}
		}

		#endregion

		#region 刷新RGB

		/// <summary>
		/// 鼠标光标在截图区域移动时触发
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMove(object sender, MouseEventArgs e)
		{
			// 获取鼠标光标当前位置
			var point = PointToScreen(e.GetPosition(this));
			AppModel.Current.ShowRGB = ImageHelper.GetImageRGB((int)point.X, (int)point.Y);
		}

		#endregion

		#region 计算图片移动的极限值

		public void ResetLimit(double left, double top, double right, double bottom)
		{
			ResetLeft(left);
			ResetTop(top);
			ResetRight(right);
			ResetBottom(bottom);
		}

		private void ResetLeft(double value)
		{
			if (value < Limit.Left)
			{
				Limit.Left = value;
			}
		}

		private void ResetTop(double value)
		{
			if (value < Limit.Top)
			{
				Limit.Top = value;
			}
		}

		private void ResetRight(double value)
		{
			if (value > Limit.Right)
			{
				Limit.Right = value;
			}
		}

		private void ResetBottom(double value)
		{
			if (value > Limit.Bottom)
			{
				Limit.Bottom = value;
			}
		}

		#endregion
	}
}
