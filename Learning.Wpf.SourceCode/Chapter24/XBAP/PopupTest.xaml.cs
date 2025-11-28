using System.Windows;
using System.Windows.Media;

namespace XBAP
{
	/// <summary>
	/// Interaction logic for PopupTest.xaml
	/// </summary>

	public partial class PopupTest : System.Windows.Controls.Page
	{
		public PopupTest()
		{
			InitializeComponent();
		}

		private void cmdStart_Click(object sender, RoutedEventArgs e)
		{
			DisableMainPage();
		}
		private void dialog_cmdOK_Click(object sender, RoutedEventArgs e)
		{
			// Copy name from the Popup into the main page.
			lblName.Content = "You entered: " + txtName.Text;

			EnableMainPage();
		}
		private void dialog_cmdCancel_Click(object sender, RoutedEventArgs e)
		{
			EnableMainPage();
		}

		private void EnableMainPage()
		{
			mainPage.IsEnabled = true;
			this.Background = null;
			dialogPopUp.IsOpen = false;
		}
		private void DisableMainPage()
		{
			mainPage.IsEnabled = false;
			this.Background = Brushes.LightGray;
			dialogPopUp.IsOpen = true;
		}

		private void cmdStartWF_Click(object sender, RoutedEventArgs e)
		{
			UserNameWinForm form = new UserNameWinForm();
			if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				lblNameWF.Content = form.UserName;
			}
			form.Dispose();
		}
	}
}