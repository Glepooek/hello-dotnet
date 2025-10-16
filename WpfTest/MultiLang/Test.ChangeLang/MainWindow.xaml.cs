using System.Windows;

namespace Test.ChangeLang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwitchLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (LangResourceManager.CurrentLanguage.Source.OriginalString.Contains("en-US"))
            {
                LangResourceManager.CurrentLanguage = new ResourceDictionary
                {
                    Source = new Uri("/Test.ChangeLang;component/Resources/Resources.zh-CN.xaml", UriKind.Relative)
                };
            }
            else
            {
                LangResourceManager.CurrentLanguage = new ResourceDictionary
                {
                    Source = new Uri("/Test.ChangeLang;component/Resources/Resources.en-US.xaml", UriKind.Relative)
                };
            }
        }

        private void ShowToast_Click(object sender, RoutedEventArgs e)
        {
            var msg = LangResourceManager.GetResourceString("MainWindow_Toast_Msg");
            MessageBox.Show(msg);
        }
    }
}