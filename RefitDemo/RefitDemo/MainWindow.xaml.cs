using CommunityToolkit.Mvvm.DependencyInjection;
using System.Diagnostics;
using System.Windows;

namespace RefitDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
            //this.Loaded += MainWindow_Loaded;
            this.Unloaded += (s,e) => 
            { 
                Trace.TraceInformation("Unloaded 0"); 
            };
            this.Closing += (s, e) =>
            {
                Trace.TraceInformation("Closing 1");
            };
            this.Closed += (s, e) =>
            {
                //App.Current.Shutdown();
                Trace.TraceInformation("Closed 2");
                App.Current.Dispatcher.Invoke(() =>
                {
                    App.Current.Shutdown();
                    Trace.TraceInformation("Dispatcher Invoke in Closed");
                }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
            };
        }
    }
}
