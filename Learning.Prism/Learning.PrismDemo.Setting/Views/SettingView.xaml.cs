using System.Windows.Controls;

namespace Learning.PrismDemo.Setting.Views
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class SettingView : UserControl
	{
		public SettingView()
		{
			InitializeComponent();
			this.Loaded += SettingView_Loaded;
		}

		private void SettingView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Focus();
		}
	}
}