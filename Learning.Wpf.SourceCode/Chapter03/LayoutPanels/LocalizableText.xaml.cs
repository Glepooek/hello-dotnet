using System.Windows;

namespace LayoutPanels
{
	/// <summary>
	/// Interaction logic for LocalizableText.xaml
	/// </summary>

	public partial class LocalizableText : Window
	{

		public LocalizableText()
		{
			InitializeComponent();
		}

		private void chkLongText_Checked(object sender, RoutedEventArgs e)
		{
			cmdPrev.Content = " <- Go to the Previous Window ";
			cmdNext.Content = " Go to the Next Window -> ";
		}

		private void chkLongText_Unchecked(object sender, RoutedEventArgs e)
		{
			cmdPrev.Content = "Prev";
			cmdNext.Content = "Next";
		}
	}
}