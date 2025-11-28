using System.Windows;
using System.Windows.Media;

namespace Windows
{
	/// <summary>
	/// Interaction logic for VistaGlassWindow.xaml
	/// </summary>

	public partial class VistaGlassWindow1 : System.Windows.Window
	{

		public VistaGlassWindow1()
		{
			InitializeComponent();
		}


		private void OnLoaded(object sender, RoutedEventArgs e)
		{

			try
			{
				VistaGlassHelper.ExtendGlass(this, -1, -1, -1, -1);

			}
			// If not Vista, paint background white.
			catch //(DllNotFoundException)
			{
				this.Background = Brushes.White;
			}
		}


	}
}