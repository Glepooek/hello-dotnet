using StoreDatabase;
using System.Collections.Generic;
using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for ListStyles.xaml
	/// </summary>

	public partial class ListStyles : System.Windows.Window
	{

		public ListStyles()
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