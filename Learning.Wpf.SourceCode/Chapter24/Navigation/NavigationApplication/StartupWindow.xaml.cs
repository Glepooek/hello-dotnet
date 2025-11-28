namespace NavigationApplication
{
	/// <summary>
	/// Interaction logic for StartupWindow.xaml
	/// </summary>

	public partial class StartupWindow : System.Windows.Navigation.NavigationWindow
	{

		public StartupWindow()
		{
			InitializeComponent();

			this.Content = new Menu();
		}

	}
}