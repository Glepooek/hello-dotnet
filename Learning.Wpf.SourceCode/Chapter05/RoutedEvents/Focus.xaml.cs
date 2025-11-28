using System;

namespace RoutedEvents
{
	/// <summary>
	/// Interaction logic for Focus.xaml
	/// </summary>

	public partial class Focus : System.Windows.Window
	{

		public Focus()
		{
			InitializeComponent();
		}
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			cmdFocus.Focus();
		}
	}
}