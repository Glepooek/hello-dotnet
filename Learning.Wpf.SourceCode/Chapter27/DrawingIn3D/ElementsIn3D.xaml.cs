using System.Windows;

namespace DrawingIn3D
{
	/// <summary>
	/// Interaction logic for ElementsIn3D.xaml
	/// </summary>

	public partial class ElementsIn3D : System.Windows.Window
	{

		public ElementsIn3D()
		{
			InitializeComponent();
		}
		private void cmd_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Button clicked.");
		}
	}
}