using StoreDatabase;
using System.Collections.Generic;
using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for MoreValueConverters.xaml
	/// </summary>

	public partial class MoreValueConverters : System.Windows.Window
	{

		public MoreValueConverters()
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