using System.Windows.Controls;

namespace Learning.PrismDemo.Call.Views
{
	/// <summary>
	/// CallView.xaml 的交互逻辑
	/// </summary>
	public partial class CallView : UserControl
	{
		public CallView()
		{
			InitializeComponent();
			this.Loaded += CallView_Loaded;
		}

		private void CallView_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//Keyboard.Focus(this);
			Focus();
		}
	}
}
