using System.Windows;

namespace CustomControlsClient
{
	/// <summary>
	/// Interaction logic for FlipPanelAlternateTemplate.xaml
	/// </summary>
	public partial class FlipPanelAlternateTemplate : Window
	{
		public FlipPanelAlternateTemplate()
		{
			InitializeComponent();
		}


		private void cmdFlip_Click(object sender, RoutedEventArgs e)
		{
			panel.IsFlipped = !panel.IsFlipped;
		}
	}
}
