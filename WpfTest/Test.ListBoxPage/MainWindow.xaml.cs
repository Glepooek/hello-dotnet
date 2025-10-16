using System.Windows;

namespace Test.ListBoxPage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();
            this.DataContext = new MainWindowVM();
        }

    }


}
