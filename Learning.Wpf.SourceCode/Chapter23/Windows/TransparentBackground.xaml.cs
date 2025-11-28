using System.Windows;
using System.Windows.Input;

namespace Windows
{
	/// <summary>
	/// Interaction logic for TransparentBackground.xaml
	/// </summary>

	public partial class TransparentBackground : System.Windows.Window
	{

		public TransparentBackground()
		{
			InitializeComponent();
		}

		private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void cmdClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}