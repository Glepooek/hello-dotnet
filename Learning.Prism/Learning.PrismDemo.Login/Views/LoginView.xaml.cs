using System.Windows.Controls;
using System.Windows.Input;

namespace Learning.PrismDemo.Login.Views
{
	/// <summary>
	/// UserControl1.xaml 的交互逻辑
	/// </summary>
	public partial class LoginView : UserControl
	{
		public LoginView()
		{
			InitializeComponent();
		}

		private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			e.Handled = true;
		}
	}
}
