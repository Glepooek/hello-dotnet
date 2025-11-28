using System.ComponentModel;
using System.Windows;


namespace NotifyIconTest
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : System.Windows.Window
	{

		public Window1()
		{
			InitializeComponent();
		}
		private void window_Closing(object sender, CancelEventArgs e)
		{
			// Don't close the window, because we might need it later.
			// Just conceal it.
			e.Cancel = true;
			this.WindowState = WindowState.Minimized;
			this.ShowInTaskbar = false;
		}
	}
}