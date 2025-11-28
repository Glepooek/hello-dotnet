using System.Windows;

namespace WPFWindowLibrary
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : System.Windows.Window
	{

		public Window1()
		{
			InitializeComponent();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello from WPF");
		}
	}
}