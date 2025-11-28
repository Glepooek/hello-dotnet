using System.Windows;
using System.Windows.Navigation;

namespace NavigationApplication
{
	/// <summary>
	/// Interaction logic for EmbeddedPage.xaml
	/// </summary>

	public partial class EmbeddedPage : System.Windows.Controls.Page
	{
		public EmbeddedPage()
		{
			InitializeComponent();
		}
		private void chkOwnsJournal_Click(object sender, RoutedEventArgs e)
		{
			if (chkOwnsJournal.IsChecked == true)
				embeddedFrame.JournalOwnership = JournalOwnership.OwnsJournal;
			else
				embeddedFrame.JournalOwnership = JournalOwnership.UsesParentJournal;
		}


	}
}