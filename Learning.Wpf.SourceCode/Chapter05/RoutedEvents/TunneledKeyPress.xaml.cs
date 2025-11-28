using System.Windows;

namespace RoutedEvents
{
	/// <summary>
	/// Interaction logic for TunneledKeyPress.xaml
	/// </summary>

	public partial class TunneledKeyPress : System.Windows.Window
	{

		public TunneledKeyPress()
		{
			InitializeComponent();
		}

		protected int eventCounter = 0;

		private void SomeKeyPressed(object sender, RoutedEventArgs e)
		{
			eventCounter++;
			string message = "#" + eventCounter.ToString() + ":\r\n" +
				" Sender: " + sender.ToString() + "\r\n" +
				" Source: " + e.Source + "\r\n" +
				" Original Source: " + e.OriginalSource + "\r\n" +
				" Event: " + e.RoutedEvent;
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