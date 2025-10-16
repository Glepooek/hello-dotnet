using GalaSoft.MvvmLight.Ioc;
using System.Windows;
using Test.DragControl.Services;
using Test.DragControl.Utils;

namespace Test.DragControl.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
             {
                 var navigation = SimpleIoc.Default.GetInstance<IFrameNavigationService>();
                 navigation.NavigateTo(PageKeyConstant.LIST_PAGE_KEY);
             };
        }
    }
}
