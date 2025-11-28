using System.IO;
using System.Windows;


namespace LoadFromCommandLine
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class FileViewer : Window
	{

		public FileViewer()
		{
			InitializeComponent();
		}

		public void LoadFile(string path)
		{
			this.Content = File.ReadAllText(path);
			this.Title = path;
		}
	}
}