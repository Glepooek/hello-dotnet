using System.Windows;
using System.Windows.Controls;

namespace Animation
{
	/// <summary>
	/// Interaction logic for RotateButtonWithLayout.xaml
	/// </summary>

	public partial class RotateButtonWithLayout : System.Windows.Window
	{

		public RotateButtonWithLayout()
		{
			InitializeComponent();
		}

		private void cmd_Clicked(object sender, RoutedEventArgs e)
		{
			lbl.Text = "You clicked: " + ((Button)e.OriginalSource).Content;
		}

	}
}