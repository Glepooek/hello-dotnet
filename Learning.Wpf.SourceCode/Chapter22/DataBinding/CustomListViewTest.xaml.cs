using System.Windows.Controls;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for CustomListViewTest.xaml
	/// </summary>

	public partial class CustomListViewTest : System.Windows.Window
	{
		public CustomListViewTest()
		{
			InitializeComponent();

			lstProducts.ItemsSource = App.StoreDb.GetProducts();

		}
		private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxItem selectedItem = (ComboBoxItem)lstView.SelectedItem;
			lstProducts.View = (ViewBase)this.FindResource(selectedItem.Content);
		}
	}
}