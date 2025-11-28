using System.Windows;

namespace Windows
{
	/// <summary>
	/// Interaction logic for SavePosition.xaml
	/// </summary>

	public partial class SavePosition : System.Windows.Window
	{
		public SavePosition()
		{
			InitializeComponent();
		}

		private void cmdSave_Click(object sender, RoutedEventArgs e)
		{
			WindowPositionHelper.SaveSize(this);
		}

		private void cmdRestore_Click(object sender, RoutedEventArgs e)
		{
			WindowPositionHelper.SetSize(this);
		}
	}
}