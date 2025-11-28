using Learning.PrismDemo.Cameras;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Learning.PrismDemo.Controls
{
	/// <summary>
	/// MediaPlayerControl.xaml 的交互逻辑
	/// </summary>
	public partial class MediaPlayerControl : UserControl
	{
		#region DependencyProperties

		#region VisibilityState

		public Visibility CloseButtonVisibility
		{
			get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
			set { SetValue(CloseButtonVisibilityProperty, value); }
		}

		public static readonly DependencyProperty CloseButtonVisibilityProperty =
			DependencyProperty.Register(nameof(CloseButtonVisibility), typeof(Visibility), typeof(MediaPlayerControl), new UIPropertyMetadata(Visibility.Visible));

		public Visibility CameraButtonVisibility
		{
			get { return (Visibility)GetValue(CameraButtonVisibilityProperty); }
			set { SetValue(CameraButtonVisibilityProperty, value); }
		}

		public static readonly DependencyProperty CameraButtonVisibilityProperty =
			DependencyProperty.Register(nameof(CameraButtonVisibility), typeof(Visibility), typeof(MediaPlayerControl), new UIPropertyMetadata(Visibility.Visible));

		public Visibility MicButtonVisibility
		{
			get { return (Visibility)GetValue(MicButtonVisibilityProperty); }
			set { SetValue(MicButtonVisibilityProperty, value); }
		}

		public static readonly DependencyProperty MicButtonVisibilityProperty =
			DependencyProperty.Register(nameof(MicButtonVisibility), typeof(Visibility), typeof(MediaPlayerControl), new UIPropertyMetadata(Visibility.Visible));

		#endregion

		#region Commands

		/// <summary>
		/// 挂断命令
		/// </summary>
		public ICommand CloseCommand
		{
			get { return (ICommand)GetValue(CloseCommandProperty); }
			set { SetValue(CloseCommandProperty, value); }
		}

		/// <summary>
		/// 挂断命令依赖项属性
		/// </summary>
		public static readonly DependencyProperty CloseCommandProperty =
			DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(MediaPlayerControl), new PropertyMetadata(null));

		#endregion

		/// <summary>
		/// 本地摄像头画面
		/// </summary>
		public ImageSource LocalImage
		{
			get { return (ImageSource)GetValue(LocalImageProperty); }
			set { SetValue(LocalImageProperty, value); }
		}

		/// <summary>
		/// 本地摄像头画面依赖项属性
		/// </summary>
		public static readonly DependencyProperty LocalImageProperty =
			DependencyProperty.Register(nameof(LocalImage), typeof(ImageSource), typeof(MediaPlayerControl), new UIPropertyMetadata(null));

		#endregion

		#region Constructor

		public MediaPlayerControl()
		{
			InitializeComponent();

			//this.Loaded += MediaPlayerControl_Loaded;
		}

		#endregion

		#region EventHandler

		private void MediaPlayerControl_Loaded(object sender, RoutedEventArgs e)
		{
			CameraManager.Instance.ConfigureCamera(1, 1280, 720, 0, 0);
			CameraManager.Instance.ReadCameraImage += CameraManager_ReadCameraImage;
			CameraManager.Instance.StartCamera();
		}

		private void CameraManager_ReadCameraImage(ImageSource image)
		{
			LocalImage = image;
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			CameraManager.Instance.StopCamera();
			LocalImage = null;
		}

		#endregion
	}
}
