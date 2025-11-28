namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataTemplateControls.xaml
	/// </summary>

	public partial class ExpandingDataTemplate : System.Windows.Window
	{

		public ExpandingDataTemplate()
		{
			InitializeComponent();
			lstCategories.ItemsSource = App.StoreDb.GetProducts();
		}


	}
}