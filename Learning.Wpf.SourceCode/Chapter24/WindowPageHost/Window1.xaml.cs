using System.Windows;


namespace WindowPageHost
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
		private void cmdClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}