using System.Windows;

namespace RoutedEvents
{
	/// <summary>
	/// Interaction logic for RoutedLabelClick.xaml
	/// </summary>

	public partial class BubbledLabelClick : System.Windows.Window
	{

		public BubbledLabelClick()
		{
			InitializeComponent();
		}

		protected int eventCounter = 0;

		private void SomethingClicked(object sender, RoutedEventArgs e)
		{
			eventCounter++;
			string message = "#" + eventCounter.ToString() + ":\r\n" +
				" Sender: " + sender.ToString() + "\r\n" +
				" Source: " + e.Source + "\r\n" +
				" Original Source: " + e.OriginalSource;
			lstMessages.Items.Add(message);
			e.Handled = (bool)chkHandle.IsChecked;
		}

		private void cmdClear_Click(object sender, RoutedEventArgs e)
		{
			eventCounter = 0;
			lstMessages.Items.Clear();
		}
	}
}