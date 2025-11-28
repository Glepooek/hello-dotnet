using System.Windows;

namespace Controls
{
	/// <summary>
	/// Interaction logic for TextBoxColumn.xaml
	/// </summary>

	public partial class ScrollableTextBoxColumn : System.Windows.Window
	{

		public ScrollableTextBoxColumn()
		{
			InitializeComponent();
		}

		private void LineUp(object sender, RoutedEventArgs e)
		{
			scroller.LineUp();
		}
		private void LineDown(object sender, RoutedEventArgs e)
		{
			scroller.LineDown();
		}
		private void PageUp(object sender, RoutedEventArgs e)
		{
			scroller.PageUp();
		}
		private void PageDown(object sender, RoutedEventArgs e)
		{
			scroller.PageDown();
		}
	}
}