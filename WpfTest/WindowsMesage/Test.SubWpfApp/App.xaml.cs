using System.Windows;

namespace Test.SubWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsProcessStarted { get; set; } = false;
        public App()
        {
            this.Startup += (s, e) =>
            {
                if (e.Args != null && e.Args.Length > 0 && e.Args[0].Equals("1"))
                {
                    IsProcessStarted = true;
                }
            };
        }

    }
}
