using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace SoundAndVideo
{
	/// <summary>
	/// Interaction logic for SynchronizedAnimation.xaml
	/// </summary>

	public partial class SynchronizedAnimation : System.Windows.Window
	{

		public SynchronizedAnimation()
		{
			InitializeComponent();
		}
		private bool suppressSeek;
		private void sliderPosition_ValueChanged(object sender, RoutedEventArgs e)
		{
			if (!suppressSeek)
				mediaStoryboard.Storyboard.Seek(DocumentRoot, TimeSpan.FromSeconds(sliderPosition.Value), TimeSeekOrigin.BeginTime);
		}
		private void media_MediaOpened(object sender, RoutedEventArgs e)
		{
			sliderPosition.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
		}

		private void storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
		{
			// Sender is the clock that was created for this storyboard.
			Clock storyboardClock = (Clock)sender;

			if (storyboardClock.CurrentProgress == null)
			{
				lblTime.Text = "[[ stopped ]]";
			}
			else
			{
				lblTime.Text = "Position: " + storyboardClock.CurrentTime.ToString();
				suppressSeek = true;
				sliderPosition.Value = storyboardClock.CurrentTime.Value.TotalSeconds;
				suppressSeek = false;
			}
		}
	}
}