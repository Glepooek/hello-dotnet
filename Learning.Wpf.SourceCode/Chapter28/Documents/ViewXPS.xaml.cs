using System.IO;
using System.Windows;
using System.Windows.Xps.Packaging;

namespace Documents
{
	/// <summary>
	/// Interaction logic for ViewXPS.xaml
	/// </summary>

	public partial class ViewXPS : System.Windows.Window
	{

		public ViewXPS()
		{
			InitializeComponent();
		}

		private void window_Loaded(object sender, RoutedEventArgs e)
		{
			XpsDocument doc = new XpsDocument("ch19.xps", FileAccess.Read);
			docViewer.Document = doc.GetFixedDocumentSequence();
			doc.Close();
		}
	}
}