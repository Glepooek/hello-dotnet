using System.Windows;

namespace WebInWpf.Cefsharp.NET452
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CsharpCallJS_Click(object sender, RoutedEventArgs e)
        {
            BrowserX.CallJavaScriptAsync("calledByCsharp('Hello JS from C#')");
        }

        private void CsharpSendMessage_Click(object sender, RoutedEventArgs e)
        {
            BrowserX.CallJavaScriptAsync("window.postMessage('Message from C#', '*');");
        }
    }
}
