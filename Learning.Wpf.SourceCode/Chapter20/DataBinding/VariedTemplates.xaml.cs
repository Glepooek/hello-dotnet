using StoreDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataTemplateList.xaml
	/// </summary>

	public partial class VariedTemplates : System.Windows.Window
	{

		public VariedTemplates()
		{
			InitializeComponent();
		}

		private ICollection<Product> products;

		private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
		{
			products = App.StoreDb.GetProducts();
			lstProducts.ItemsSource = products;
		}

		private void cmdApplyChange_Click(object sender, RoutedEventArgs e)
		{
			((ObservableCollection<Product>)products)[1].CategoryName = "Travel";
			DataTemplateSelector selector = lstProducts.ItemTemplateSelector;
			lstProducts.ItemTemplateSelector = null;
			lstProducts.ItemTemplateSelector = selector;
		}
	}
}