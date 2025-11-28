using System.Windows;

namespace SingleInstanceApplication
{
	/// <summary>
	/// Interaction logic for DocumentList.xaml
	/// </summary>

	public partial class DocumentList : System.Windows.Window
	{
		public DocumentList()
		{
			InitializeComponent();

			// Show the window names in a list.
			lstDocuments.DisplayMemberPath = "Name";
			lstDocuments.ItemsSource = ((WpfApp)Application.Current).Documents;
		}
	}
}