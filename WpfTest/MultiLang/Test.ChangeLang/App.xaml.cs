using System.Windows;

namespace Test.ChangeLang
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LangResourceManager.CurrentLanguage = new ResourceDictionary
            {
                Source = new Uri("/Test.ChangeLang;component/Resources/Resources.en-US.xaml", UriKind.Relative)
            };
            base.OnStartup(e);
        }
    }

}
