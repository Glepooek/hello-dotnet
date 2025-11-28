using System.Windows.Controls;

namespace Stylet.Samples.OverridingViewManager
{
    /// <summary>
    /// Page1View.xaml 的交互逻辑
    /// </summary>
    [ViewModel(typeof(Page1ViewModel))]
    public partial class Page1View : UserControl
    {
        public Page1View()
        {
            InitializeComponent();
        }
    }
}
