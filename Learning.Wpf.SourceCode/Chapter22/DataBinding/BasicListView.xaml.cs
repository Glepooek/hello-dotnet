namespace DataBinding
{
	/// <summary>
	/// Interaction logic for BasicListView.xaml
	/// </summary>

	public partial class BasicListView : System.Windows.Window
	{


		public BasicListView()
		{
			InitializeComponent();

			lstProducts.ItemsSource = App.StoreDb.GetProducts();
		}

	}
}