using System.Runtime.InteropServices;
using System.Windows;

namespace WebBrowserTest
{
	/// <summary>
	/// Interaction logic for CallWpfWithJavaScript.xaml
	/// </summary>
	public partial class CallWpfWithJavaScript : Window
	{
		public CallWpfWithJavaScript()
		{
			InitializeComponent();

			webBrowser.Navigate("file:///" +
				System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ResourceAssembly.Location), "sample.htm"));
			webBrowser.ObjectForScripting = new HtmlBridge();
		}
	}

	[ComVisible(true)]
	public class HtmlBridge
	{
		public void WebClick(string source)
		{
			MessageBox.Show("Received: " + source);
		}
	}

}
