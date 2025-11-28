using System.Windows;

namespace WPFWindowLibrary
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>

	public partial class UserControl1 : System.Windows.Controls.UserControl
	{

		public UserControl1()
		{
			InitializeComponent();
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Hello from WPF");
		}
	}
}