using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Data;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for FilterDataSet.xaml
	/// </summary>

	public partial class FilterDataSet : System.Windows.Window
	{

		public FilterDataSet()
		{
			InitializeComponent();
		}

		private DataTable products;

		private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
		{
			products = App.StoreDbDataSet.GetProducts();
			lstProducts.ItemsSource = products.DefaultView;

			ICollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource);
			view.SortDescriptions.Add(new SortDescription("ModelName", ListSortDirection.Ascending));
		}

		private void cmdFilter_Click(object sender, RoutedEventArgs e)
		{
			decimal minimumPrice;
			if (Decimal.TryParse(txtMinPrice.Text, out minimumPrice))
			{
				BindingListCollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource) as BindingListCollectionView;
				if (view != null)
				{
					view.CustomFilter = "UnitCost > " + minimumPrice.ToString();
				}
			}
		}

		private void cmdRemoveFilter_Click(object sender, RoutedEventArgs e)
		{
			BindingListCollectionView view = CollectionViewSource.GetDefaultView(lstProducts.ItemsSource) as BindingListCollectionView;
			if (view != null)
			{
				view.CustomFilter = "";
			}
		}
	}
}