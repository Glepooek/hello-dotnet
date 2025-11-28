using System.Windows;


namespace SimpleWindow
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

		private void cmd_Click(object sender, RoutedEventArgs e)
		{
			VisualTreeDisplay treeDisplay = new VisualTreeDisplay();
			treeDisplay.ShowVisualTree(this);
			treeDisplay.Show();

		}
	}
}