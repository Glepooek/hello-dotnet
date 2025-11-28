using System.Windows;
using System.Windows.Media;

namespace Windows
{
	/// <summary>
	/// Interaction logic for VistaGlassWindow.xaml
	/// </summary>

	public partial class VistaGlassWindow2 : System.Windows.Window
	{

		public VistaGlassWindow2()
		{
			InitializeComponent();
		}


		private void OnLoaded(object sender, RoutedEventArgs e)
		{

			try
			{
				VistaGlassHelper.ExtendGlass(this, 5, 5, (int)topBar.ActualHeight + 5, 5);

			}
			// If not Vista, paint background white.
			catch //(DllNotFoundException)
			{
				this.Background = Brushes.White;
			}
		}


	}
}