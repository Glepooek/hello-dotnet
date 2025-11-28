using System.Data;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for BoundTreeView.xaml
	/// </summary>

	public partial class BoundTreeViewDataSet : System.Windows.Window
	{

		public BoundTreeViewDataSet()
		{
			InitializeComponent();

			DataSet ds = App.StoreDbDataSet.GetCategoriesAndProducts();

			treeCategories.ItemsSource = ds.Tables["Categories"].DefaultView;
		}

	}
}