namespace DataBinding
{
	/// <summary>
	/// Interaction logic for BoundTreeView.xaml
	/// </summary>

	public partial class BoundTreeView : System.Windows.Window
	{

		public BoundTreeView()
		{
			InitializeComponent();

			treeCategories.ItemsSource = App.StoreDb.GetCategoriesAndProducts();
		}

	}
}