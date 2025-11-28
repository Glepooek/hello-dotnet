using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Printing
{
	/// <summary>
	/// Interaction logic for PrintCustomPages.xaml
	/// </summary>

	public partial class PrintCustomPages : System.Windows.Window
	{

		public PrintCustomPages()
		{
			InitializeComponent();
		}

		private void cmdPrint_Click(object sender, RoutedEventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();
			if (printDialog.ShowDialog() == true)
			{
				DataSet ds = new DataSet();
				ds.ReadXmlSchema("store.xsd");
				ds.ReadXml("store.xml");

				StoreDataSetPaginator paginator = new StoreDataSetPaginator(ds.Tables[0],
					new Typeface("Calibri"), 24, 96 * 0.75,
					new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));

				printDialog.PrintDocument(paginator, "Custom-Printed Pages");
			}

		}
	}
}