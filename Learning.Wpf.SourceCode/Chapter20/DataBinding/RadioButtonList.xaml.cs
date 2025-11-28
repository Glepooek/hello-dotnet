using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for RadioButtonList.xaml
	/// </summary>

	public partial class RadioButtonList : System.Windows.Window
	{

		public RadioButtonList()
		{
			InitializeComponent();

			lstProducts.ItemsSource = App.StoreDb.GetProducts();
		}

		private void cmdGetSelectedItem(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(lstProducts.SelectedItem.ToString());
		}
	}
}