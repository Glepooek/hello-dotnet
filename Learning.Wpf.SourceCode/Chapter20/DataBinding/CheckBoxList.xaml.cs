using StoreDatabase;
using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for CheckBoxList.xaml
	/// </summary>

	public partial class CheckBoxList : System.Windows.Window
	{

		public CheckBoxList()
		{
			InitializeComponent();

			lstProducts.ItemsSource = App.StoreDb.GetProducts();
		}

		private void cmdGetSelectedItems(object sender, RoutedEventArgs e)
		{
			if (lstProducts.SelectedItems.Count > 0)
			{
				string items = "You selected: ";
				foreach (Product product in lstProducts.SelectedItems)
				{
					items += "\n  * " + product.ModelName;
				}
				MessageBox.Show(items);
			}
		}
	}
}