using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.MusicStore.ViewModels;
using Avalonia.MusicStore.Views;
using System.Globalization;
using System.Threading;

namespace Avalonia.MusicStore
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avalonia 12 disables the DataAnnotations validation plugin by default,
                // so the previous BindingPlugins.DataValidators.RemoveAt(0) workaround is no longer needed.
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}