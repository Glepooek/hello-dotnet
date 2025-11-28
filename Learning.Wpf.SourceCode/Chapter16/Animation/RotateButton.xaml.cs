using System.Windows;
using System.Windows.Controls;

namespace Animation
{
	/// <summary>
	/// Interaction logic for RotateButton.xaml
	/// </summary>

	public partial class RotateButton : System.Windows.Window
	{

		public RotateButton()
		{
			InitializeComponent();
		}

		private void cmd_Clicked(object sender, RoutedEventArgs e)
		{
			lbl.Text = "You clicked: " + ((Button)e.OriginalSource).Content;
		}

	}
}