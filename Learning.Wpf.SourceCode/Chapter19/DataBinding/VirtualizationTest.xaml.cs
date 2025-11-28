using System.Windows;

namespace DataBinding
{
	/// <summary>
	/// Interaction logic for VirtualizationTest.xaml
	/// </summary>
	public partial class VirtualizationTest : Window
	{
		public VirtualizationTest()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < 10000; i++)
			{
				lstFast.Items.Add(i.ToString());
				lstSlow.Items.Add(i.ToString());
			}
		}
	}
}
