using System.Windows;

namespace WindowTracker
{
	/// <summary>
	/// Interaction logic for Document.xaml
	/// </summary>

	public partial class Document : Window
	{
		public Document()
		{
			InitializeComponent();
		}

		public void SetContent(string content)
		{
			this.Content = content;
		}
	}
}