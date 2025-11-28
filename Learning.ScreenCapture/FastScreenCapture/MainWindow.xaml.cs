using System.Windows;

namespace FastScreenCapture
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ScreenCaptureWindow ScreenCapture = new ScreenCaptureWindow();
			ScreenCapture.Show();
		}
	}
}
