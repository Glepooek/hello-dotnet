using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for DataTemplateControls.xaml
	/// </summary>

	public partial class DataTemplateControls : System.Windows.Window
	{

		public DataTemplateControls()
		{
			InitializeComponent();
			lstCategories.ItemsSource = App.StoreDbDataSet.GetCategoriesAndProducts().Tables["Categories"].DefaultView;
		}

		private void cmdView_Clicked(object sender, RoutedEventArgs e)
		{
			Button cmd = (Button)sender;
			DataRowView row = (DataRowView)cmd.Tag;
			lstCategories.SelectedItem = row;

			// Alternate selection approach.
			//ListBoxItem item = (ListBoxItem)lstCategories.ItemContainerGenerator.ContainerFromItem(row);
			//item.IsSelected = true;

			MessageBox.Show("You chose category #" + row["CategoryID"].ToString() + ": " + (string)row["CategoryName"]);
		}
	}
}