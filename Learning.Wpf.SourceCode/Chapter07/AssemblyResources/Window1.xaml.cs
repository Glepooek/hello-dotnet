using System;
using System.Windows;
using System.Windows.Media.Imaging;


namespace AssemblyResources
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : System.Windows.Window
	{

		public Window1()
		{
			InitializeComponent();
		}

		private void cmdPlay_Click(object sender, RoutedEventArgs e)
		{

			img.Source = new BitmapImage(new Uri("images/winter.jpg", UriKind.Relative));
			//img.Source = new BitmapImage(new Uri("pack://application:,,,/images/winter.jpg"));
			Sound.Stop();
			Sound.Play();

		}
	}
}