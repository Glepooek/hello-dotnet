using System;
using System.Windows;


namespace WindowTracker
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void cmdCreate_Click(object sender, RoutedEventArgs e)
		{
			Document doc = new Document();
			doc.Owner = this;
			doc.Show();
			((App)Application.Current).Documents.Add(doc);
		}

		private void cmdUpdate_Click(object sender, RoutedEventArgs e)
		{
			foreach (Document doc in ((App)Application.Current).Documents)
			{
				doc.SetContent("Refreshed at " + DateTime.Now.ToLongTimeString() + ".");
			}
		}
	}
}