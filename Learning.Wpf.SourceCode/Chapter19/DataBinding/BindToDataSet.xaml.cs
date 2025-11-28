using System.Data;
using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for BindToDataSet.xaml
	/// </summary>

	public partial class BindToDataSet : System.Windows.Window
	{

		public BindToDataSet()
		{
			InitializeComponent();
		}


		private DataTable products;

		private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
		{
			products = App.StoreDbDataSet.GetProducts();
			lstProducts.ItemsSource = products.DefaultView;
		}

		private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
		{
			((DataRowView)lstProducts.SelectedItem).Row.Delete();
		}


	}
}