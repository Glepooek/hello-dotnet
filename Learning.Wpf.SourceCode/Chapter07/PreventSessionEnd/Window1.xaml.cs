using System;
using System.Windows;


namespace PreventSessionEnd
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : Window
	{

		public Window1()
		{
			InitializeComponent();
		}

		private void cmdException_Click(object sender, RoutedEventArgs e)
		{
			throw new ApplicationException("You clicked a bad button.");
		}
	}
}