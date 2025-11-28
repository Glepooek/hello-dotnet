using System.Windows;
using System.Windows.Controls;

namespace RoutedEvents
{
	/// <summary>
	/// Interaction logic for ButtonMouseUpEvent.xaml
	/// </summary>

	public partial class ButtonMouseUpEvent : System.Windows.Window
	{

		public ButtonMouseUpEvent()
		{
			InitializeComponent();
			cmd.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(Backdoor), true);
		}

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("The Button.Click event occurred. This may have been triggered with the keyboard.");
		}

		private void NeverCalled(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("You didn't see this message. That would be impossible.");
		}

		private void Backdoor(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("The (handled) Button.MouseUp event occurred.");
		}


	}
}