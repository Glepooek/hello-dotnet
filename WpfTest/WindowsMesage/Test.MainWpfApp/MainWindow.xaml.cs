using System.Windows;
using Test.Shared;

namespace Test.MainWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                //WindowsMessageHelper.SendMessageByProcessName("Test.SubWpfApp", "Play");
                WindowsMessageHelper.SendMessageByMainWindowName("Test.SubWindow", "Play");
            };
        }
    }
}
