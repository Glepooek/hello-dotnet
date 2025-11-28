using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataGridGrouping.xaml
	/// </summary>
	public partial class DataGridGrouping : Window
	{
		public DataGridGrouping()
		{
			InitializeComponent();

			gridProducts.ItemsSource = App.StoreDb.GetProducts();
			ICollectionView view = CollectionViewSource.GetDefaultView(gridProducts.ItemsSource);

			view.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
		}
	}
}
