using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Windows7_TaskBar
{
	/// <summary>
	/// Interaction logic for ThumbnailButtons.xaml
	/// </summary>
	public partial class ThumbnailButtons : Window
	{
		public ThumbnailButtons()
		{
			InitializeComponent();
		}

		private void cmdPlay_Click(object sender, EventArgs e)
		{
			taskBarItem.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
			taskBarItem.Overlay = new BitmapImage(
  new Uri("pack://application:,,,/play.png"));
		}

		private void cmdPause_Click(object sender, EventArgs e)
		{
			taskBarItem.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
			taskBarItem.Overlay = new BitmapImage(
  new Uri("pack://application:,,,/pause.png"));
		}


	}
}
