namespace InteroperabilityWPF
{
	/// <summary>
	/// Interaction logic for HostWinFormControl.xaml
	/// </summary>

	public partial class HostWinFormControl : System.Windows.Window
	{

		public HostWinFormControl()
		{
			InitializeComponent();
		}

		private void maskedTextBox_MaskInputRejected(object sender, System.Windows.Forms.MaskInputRejectedEventArgs e)
		{
			lblErrorText.Content = "Error: " + e.RejectionHint.ToString();
		}
	}
}