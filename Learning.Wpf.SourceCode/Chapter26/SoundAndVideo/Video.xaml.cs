using System;
using System.Windows;

namespace SoundAndVideo
{
	/// <summary>
	/// Interaction logic for Video.xaml
	/// </summary>

	public partial class Video : System.Windows.Window
	{

		public Video()
		{
			InitializeComponent();

		}

		private void cmdPlay_Click(object sender, RoutedEventArgs e)
		{
			video.Position = TimeSpan.Zero;
			video.Play();
		}

	}
}