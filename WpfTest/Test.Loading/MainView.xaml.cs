using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace Test.Loading
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private MainViewModel mMainViewModel;
        public MainView()
        {
            InitializeComponent();
            mMainViewModel = new MainViewModel();
            this.DataContext = mMainViewModel;

            Run run = new Run();
            Binding companyNameBinding = new Binding("CompanyName");
            run.SetBinding(Run.TextProperty, companyNameBinding);
            Binding fontSizeBinding = new Binding("FontSize");
            run.SetBinding(Run.FontSizeProperty, fontSizeBinding);
            test.Inlines.Add(run);
        }

        private void OnChangeCompanyNameClick(object sender, RoutedEventArgs e)
        {
            mMainViewModel.CompanyName = "外研社";
        }

        private void OnChangeCompanyNameFontSizeClick(object sender, RoutedEventArgs e)
        {
            mMainViewModel.FontSize += 5;
        }
    }
}
