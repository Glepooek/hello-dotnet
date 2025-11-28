using System.Windows;
using System.Windows.Controls;

namespace LayoutPanels
{
	/// <summary>
	/// Interaction logic for SimpleStack.xaml
	/// </summary>

	public partial class SimpleStack : Window
	{

		public SimpleStack()
		{
			InitializeComponent();
		}

		private void chkVertical_Checked(object sender, RoutedEventArgs e)
		{
			stackPanel1.Orientation = Orientation.Horizontal;
		}

		private void chkVertical_Unchecked(object sender, RoutedEventArgs e)
		{
			stackPanel1.Orientation = Orientation.Vertical;
		}
	}
}