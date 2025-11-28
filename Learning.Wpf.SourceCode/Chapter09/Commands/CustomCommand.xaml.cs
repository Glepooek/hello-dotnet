using System.Windows;
using System.Windows.Input;

namespace Commands
{
	/// <summary>
	/// Interaction logic for CustomCommand.xaml
	/// </summary>

	public partial class CustomCommand : System.Windows.Window
	{

		public CustomCommand()
		{
			InitializeComponent();
		}


		private void RequeryCommand(object sender, ExecutedRoutedEventArgs e)
		{
			MessageBox.Show("Requery");
		}

	}

	public class DataCommands
	{
		private static RoutedUICommand requery;
		static DataCommands()
		{
			InputGestureCollection inputs = new InputGestureCollection();
			inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
			requery = new RoutedUICommand(
			  "Requery", "Requery", typeof(DataCommands), inputs);
		}

		public static RoutedUICommand Requery
		{
			get { return requery; }
		}
	}

}