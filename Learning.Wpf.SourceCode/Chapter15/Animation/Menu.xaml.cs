using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Animation
{
	public partial class Menu : Window
	{
		public Menu()
		{
			InitializeComponent();
		}

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			// Get the current button.
			Button btn = (Button)e.OriginalSource;

			// Create an instance of the window named
			// by the current button.
			Type type = this.GetType();
			Assembly assembly = type.Assembly;
			Window win = (Window)assembly.CreateInstance($"{type.Namespace}.{btn.Content}");

			// Show the window.
			win.ShowDialog();
		}
	}
}