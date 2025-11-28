using System.Windows;


namespace LocalizableApplication
{
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : System.Windows.Window
	{

		public Window1()
		{
			InitializeComponent();
		}

		protected void cmd_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(this.Resources["Error"].ToString());
		}
	}
}