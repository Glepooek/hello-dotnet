using System.Windows;

namespace _06_ConfigureAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 执行某个可等待的方法，只有该方法的返回结果不需要调度到UI线程事才应该使用ConfigureAwait(false)
        }
    }
}
