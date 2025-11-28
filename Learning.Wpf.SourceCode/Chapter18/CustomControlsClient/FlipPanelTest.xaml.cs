using System.Windows;

namespace CustomControlsClient
{
	/// <summary>
	/// Interaction logic for FlipPanelTest.xaml
	/// </summary>
	public partial class FlipPanelTest : Window
	{
		public FlipPanelTest()
		{
			InitializeComponent();
		}

		private void cmdFlip_Click(object sender, RoutedEventArgs e)
		{
			panel.IsFlipped = !panel.IsFlipped;
		}
	}
}
