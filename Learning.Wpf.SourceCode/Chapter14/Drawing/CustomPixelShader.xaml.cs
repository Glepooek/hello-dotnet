using System.Windows;

namespace Drawing
{
	/// <summary>
	/// Interaction logic for CustomPixelShader.xaml
	/// </summary>
	public partial class CustomPixelShader : Window
	{
		public CustomPixelShader()
		{
			InitializeComponent();
		}

		private void chkEffect_Click(object sender, RoutedEventArgs e)
		{
			if (chkEffect.IsChecked != true)
				img.Effect = null;
			else
				img.Effect = new GrayscaleEffect();
		}
	}
}
