using System.Windows;

namespace Windows
{
	/// <summary>
	/// Interaction logic for CenterScreen.xaml
	/// </summary>

	public partial class CenterScreen : System.Windows.Window
	{
		public CenterScreen()
		{
			InitializeComponent();
		}

		private void cmdCenter_Click(object sender, RoutedEventArgs e)
		{
			double height = SystemParameters.WorkArea.Height;
			double width = SystemParameters.WorkArea.Width;
			this.Top = (height - this.Height) / 2;
			this.Left = (width - this.Width) / 2;
		}
	}
}