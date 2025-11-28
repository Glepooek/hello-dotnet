using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataGridRowDetails.xaml
	/// </summary>
	public partial class DataGridRowDetails : Window
	{
		public DataGridRowDetails()
		{
			InitializeComponent();

			gridProducts.ItemsSource = App.StoreDb.GetProducts();
			;
		}
	}
}
