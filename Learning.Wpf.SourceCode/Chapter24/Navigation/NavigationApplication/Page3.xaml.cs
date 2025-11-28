using System;
using System.Windows.Navigation;

namespace NavigationApplication
{
	/// <summary>
	/// Interaction logic for Page3.xaml
	/// </summary>

	public partial class Page3 : System.Windows.Controls.Page
	{
		public Page3()
		{
			InitializeComponent();
		}

		private void Init(object sender, EventArgs e)
		{
			NavigationService nav = NavigationService.GetNavigationService(this);
			while (nav.CanGoBack)
			{
				JournalEntry obj = nav.RemoveBackEntry();
			}
		}
	}
}