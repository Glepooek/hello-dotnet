using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Styles
{
	/// <summary>
	/// Interaction logic for EventSetter.xaml
	/// </summary>

	public partial class EventSetter : System.Windows.Window
	{

		public EventSetter()
		{
			InitializeComponent();
		}

		private void element_MouseEnter(object sender, MouseEventArgs e)
		{
			((TextBlock)sender).Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
		}
		private void element_MouseLeave(object sender, MouseEventArgs e)
		{
			((TextBlock)sender).Background = null;
		}
	}
}