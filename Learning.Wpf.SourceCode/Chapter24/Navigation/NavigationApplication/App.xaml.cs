using System.Windows;
using System.Windows.Navigation;

namespace NavigationApplication
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>

	public partial class App : System.Windows.Application
	{
		private void App_NavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			if (e.Exception is System.Net.WebException)
			{
				MessageBox.Show("Website " + e.Uri.ToString() + " cannot be reached.");

				// Neutralize the erorr so the application continues running.
				e.Handled = true;
			}
		}

	}
}