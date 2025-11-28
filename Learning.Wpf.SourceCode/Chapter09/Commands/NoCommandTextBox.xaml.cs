using System.Windows.Input;

namespace Commands
{
	/// <summary>
	/// Interaction logic for NoCommandTextBox.xaml
	/// </summary>

	public partial class NoCommandTextBox : System.Windows.Window
	{

		public NoCommandTextBox()
		{
			InitializeComponent();

			txt.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, null, SuppressCommand));

			txt.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.C, ModifierKeys.Control));
			//txt.ContextMenu = null;
		}

		private void SuppressCommand(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			e.Handled = true;
		}
	}
}