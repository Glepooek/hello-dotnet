using StoreDatabase;
using System.Collections.Generic;
using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataTemplateList.xaml
	/// </summary>

	public partial class DataTemplateImages : System.Windows.Window
	{

		public DataTemplateImages()
		{
			InitializeComponent();
		}

		private ICollection<Product> products;

		private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
		{
			products = App.StoreDb.GetProducts();
			lstProducts.ItemsSource = products;

		}

	}
}