using System.Windows;
using System.Windows.Forms.Integration;


namespace InteroperabilityWPF
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

		private void cmdShowWinForm_Click(object sender, RoutedEventArgs e)
		{
			Form1 frm = new Form1();
			frm.Show();
		}

		private void cmdShowHostWinFormControl_Click(object sender, RoutedEventArgs e)
		{
			HostWinFormControl win = new HostWinFormControl();
			win.Show();
		}


		private void cmdEnableSupport_Click(object sender, RoutedEventArgs e)
		{
			WindowsFormsHost.EnableWindowsFormsInterop();
		}

		private void cmdShowModalWinForm_Click(object sender, RoutedEventArgs e)
		{
			Form1 frm = new Form1();
			if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				MessageBox.Show("You clicked OK.");
			}
		}
		private void cmdShowWPFMnemonic_Click(object sender, RoutedEventArgs e)
		{
			MnemonicTest win = new MnemonicTest();
			win.ShowDialog();
		}
		private void cmdShowWinFormMnemonic_Click(object sender, RoutedEventArgs e)
		{
			MnemonicTest2 frm = new MnemonicTest2();
			frm.ShowDialog();
		}

	}
}