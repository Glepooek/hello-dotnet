using System.Windows;

namespace Stylet.Samples.OverridingViewManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ViewModel(typeof(ShellViewModel))]
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }
    }
}
