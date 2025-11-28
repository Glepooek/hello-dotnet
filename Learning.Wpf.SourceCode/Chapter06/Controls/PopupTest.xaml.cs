using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Controls
{
	/// <summary>
	/// Interaction logic for PopupTest.xaml
	/// </summary>

	public partial class PopupTest : System.Windows.Window
	{

		public PopupTest()
		{
			InitializeComponent();
		}

		private void run_MouseEnter(object sender, MouseEventArgs e)
		{
			popLink.IsOpen = true;
		}
		private void lnk_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(((Hyperlink)sender).NavigateUri.ToString());
		}
	}
}