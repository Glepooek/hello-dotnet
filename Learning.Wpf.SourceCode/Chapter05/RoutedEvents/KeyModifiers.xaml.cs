using System.Windows;
using System.Windows.Input;

namespace RoutedEvents
{
	/// <summary>
	/// Interaction logic for KeyModifiers.xaml
	/// </summary>

	public partial class KeyModifiers : System.Windows.Window
	{

		public KeyModifiers()
		{
			InitializeComponent();
		}

		private void KeyEvent(object sender, KeyEventArgs e)
		{

			lblInfo.Text = "Modifiers: " +
				e.KeyboardDevice.Modifiers.ToString();

			if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
			{
				lblInfo.Text += "\r\nYou held the Control key.";
			}
		}

		private void CheckShift(object sender, RoutedEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.LeftShift))
			{
				lblInfo.Text = "The left Shift is held down.";
			}
			else
			{
				lblInfo.Text = "The left Shift is not held down.";
			}

		}
	}
}