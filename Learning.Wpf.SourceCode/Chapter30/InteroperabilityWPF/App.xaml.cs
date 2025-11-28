using System.Windows;

namespace InteroperabilityWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>

	public partial class App : System.Windows.Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			// Raises the Startup event.
			base.OnStartup(e);

			System.Windows.Forms.Application.EnableVisualStyles();
		}
	}
}