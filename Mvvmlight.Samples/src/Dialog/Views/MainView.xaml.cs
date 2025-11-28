using Dialog.Services;
using GalaSoft.MvvmLight.Ioc;
using System.Windows;

namespace Dialog.Views
{
    /// <summary>
    /// 主视图交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                var navigation = SimpleIoc.Default.GetInstance<IFrameNavigationService>();
                navigation.NavigateTo(PageKeyConstant.PageOne);
            };
        }
    }
}
