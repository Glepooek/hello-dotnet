using System;
using System.IO;
using System.Windows;


namespace SingleInstanceApplication
{
	public partial class Document : Window
	{
		private DocumentReference docRef;

		public Document()
		{
			InitializeComponent();
		}

		public void LoadFile(DocumentReference docRef)
		{
			this.docRef = docRef;
			this.Content = File.ReadAllText(docRef.Name);
			this.Title = docRef.Name;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			((WpfApp)Application.Current).Documents.Remove(docRef);
		}
	}
}