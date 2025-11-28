using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for ElementToElementBinding.xaml
	/// </summary>

	public partial class ElementToElementBinding : System.Windows.Window
	{

		public ElementToElementBinding()
		{
			InitializeComponent();
		}

		private void cmd_SetSmall(object sender, RoutedEventArgs e)
		{
			sliderFontSize.Value = 2;
		}

		private void cmd_SetNormal(object sender, RoutedEventArgs e)
		{
			sliderFontSize.Value = this.FontSize;
		}

		private void cmd_SetLarge(object sender, RoutedEventArgs e)
		{
			// Only works in two-way mode.
			lblSampleText.FontSize = 30;

		}
	}
}