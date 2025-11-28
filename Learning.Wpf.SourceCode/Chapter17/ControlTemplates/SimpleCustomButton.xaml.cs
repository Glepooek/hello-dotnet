using System.Windows;
using System.Windows.Controls;

namespace ControlTemplates
{
	/// <summary>
	/// Interaction logic for SimpleCustomButton.xaml
	/// </summary>

	public partial class SimpleCustomButton : System.Windows.Window
	{

		public SimpleCustomButton()
		{
			InitializeComponent();
		}

		private void Clicked(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("You clicked " + ((Button)sender).Name);
		}

	}
}