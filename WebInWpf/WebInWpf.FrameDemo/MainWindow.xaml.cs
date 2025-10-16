using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebInWpf.FrameDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s,e) =>
            {
                //frame.Navigate(new Uri("file:///E:/ProjectxPlex/WPFCodePlex/WebInWpf/WebInWpf/WebInWpf.FrameDemo/bin/Debug/net8.0-windows/TestWeb/index.html"));
            };
        }
    }
}