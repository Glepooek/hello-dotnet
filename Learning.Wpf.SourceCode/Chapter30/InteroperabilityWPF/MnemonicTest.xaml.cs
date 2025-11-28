using System;
using System.Windows;

namespace InteroperabilityWPF
{
	/// <summary>
	/// Interaction logic for MnemonicTest.xaml
	/// </summary>

	public partial class MnemonicTest : System.Windows.Window
	{

		public MnemonicTest()
		{
			InitializeComponent();
		}

		private void cmdClicked(object sender, EventArgs e)
		{
			MessageBox.Show(sender.ToString());

		}
	}
}