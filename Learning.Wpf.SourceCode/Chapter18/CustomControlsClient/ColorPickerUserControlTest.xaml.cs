using System.Windows;
using System.Windows.Media;

namespace CustomControlsClient
{
	/// <summary>
	/// Interaction logic for ColorPickerUserControlTest.xaml
	/// </summary>

	public partial class ColorPickerUserControlTest : System.Windows.Window
	{

		public ColorPickerUserControlTest()
		{
			InitializeComponent();
		}

		private void cmdGet_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(colorPicker.Color.ToString());
		}
		private void cmdSet_Click(object sender, RoutedEventArgs e)
		{
			colorPicker.Color = Colors.Beige;
		}

		private void colorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			if (lblColor != null)
				lblColor.Text = "The new color is " + e.NewValue.ToString();
		}

	}
}