using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Printing
{
	/// <summary>
	/// Interaction logic for PrintWithoutUserIntervention.xaml
	/// </summary>

	public partial class PrintWithoutUserIntervention : System.Windows.Window
	{

		public PrintWithoutUserIntervention()
		{
			InitializeComponent();
		}

		private void cmdPrint_Click(object sender, RoutedEventArgs e)
		{
			PrintDialog dialog = new PrintDialog();
			dialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
			dialog.PrintVisual((Visual)sender, "Automatic Printout");
		}
	}
}