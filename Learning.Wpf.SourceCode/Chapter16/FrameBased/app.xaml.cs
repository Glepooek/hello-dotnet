using System.Windows;

namespace Microsoft.Samples.PerFrameAnimations
{

	// Displays the sample.
	public partial class app : Application
	{

		public app()
		{

		}


		protected override void OnStartup(StartupEventArgs e)
		{

			Window w = new Window();
			w.Content = new SampleViewer();
			w.Show();

			base.OnStartup(e);
		}




	}

}