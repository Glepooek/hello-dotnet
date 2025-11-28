using System.Windows;

namespace Learning.SharpDX.ShowInD3DImage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DcwtTmmwvcr.Loaded += (s, e) =>
            {
                KsyosqStmckfy.CreateAndBindTargets((int)ActualWidth, (int)ActualHeight);
            };
        }
    }
}
