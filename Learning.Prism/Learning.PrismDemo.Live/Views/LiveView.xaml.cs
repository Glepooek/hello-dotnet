using System.Windows.Controls;

namespace Learning.PrismDemo.Live.Views
{
	/// <summary>
	/// LiveView.xaml 的交互逻辑
	/// </summary>
	public partial class LiveView : UserControl
	{
		public LiveView()
		{
			InitializeComponent();
			this.Loaded += LiveView_Loaded;
		}

		private void LiveView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Focus();
		}
	}
}
