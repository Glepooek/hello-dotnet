using StoreDatabase;
using System.Collections.Generic;
using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for WrappedList.xaml
	/// </summary>

	public partial class WrappedList : System.Windows.Window
	{

		public WrappedList()
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