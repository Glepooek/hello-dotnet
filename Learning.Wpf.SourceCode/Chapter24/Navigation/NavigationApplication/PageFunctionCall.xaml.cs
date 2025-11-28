using System.Windows;
using System.Windows.Navigation;

namespace NavigationApplication
{
	/// <summary>
	/// Interaction logic for PageFunctionCall.xaml
	/// </summary>

	public partial class PageFunctionCall : System.Windows.Controls.Page
	{
		public PageFunctionCall()
		{
			InitializeComponent();
		}
		private void cmdSelect_Click(object sender, RoutedEventArgs e)
		{
			SelectProductPageFunction pageFunction = new SelectProductPageFunction();
			pageFunction.Return += new ReturnEventHandler<Product>(
			  SelectProductPageFunction_Returned);
			this.NavigationService.Navigate(pageFunction);

		}
		private void SelectProductPageFunction_Returned(object sender,
		  ReturnEventArgs<Product> e)
		{
			if (e != null)
				lblStatus.Content = "You chose: " + e.Result.Name;
		}

	}
}