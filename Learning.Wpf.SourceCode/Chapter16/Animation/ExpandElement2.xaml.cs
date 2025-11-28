using System;
using System.Windows;

namespace Animation
{
	/// <summary>
	/// Interaction logic for ExpandElement2.xaml
	/// </summary>

	public partial class ExpandElement2 : System.Windows.Window
	{

		public ExpandElement2()
		{
			InitializeComponent();
		}

		private void storyboardCompleted(object sender, EventArgs e)
		{
			rectangle.Visibility = Visibility.Collapsed;
		}

	}
}