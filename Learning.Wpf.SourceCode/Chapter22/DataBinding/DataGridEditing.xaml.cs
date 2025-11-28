using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataGridEditing.xaml
	/// </summary>
	public partial class DataGridEditing : Window
	{
		public DataGridEditing()
		{

			InitializeComponent();
			categoryColumn.ItemsSource = App.StoreDb.GetCategories();
			gridProducts.ItemsSource = App.StoreDb.GetProducts();

		}




	}
}
