using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for AdvancedListView.xaml
	/// </summary>

	public partial class AdvancedListView : System.Windows.Window
	{

		public AdvancedListView()
		{
			InitializeComponent();

			lstProducts.ItemsSource = App.StoreDb.GetProducts();
		}



		private void gridViewColumn_Click(object sender, RoutedEventArgs e)
		{

		}




	}
}