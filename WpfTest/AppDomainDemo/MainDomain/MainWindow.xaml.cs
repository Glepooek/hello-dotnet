using PluginManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainDomain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ComponentManager manager = null;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            manager?.Unload();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WriteLine($"{AppDomain.CurrentDomain.FriendlyName}");
            WriteLine($"{AppDomain.CurrentDomain.BaseDirectory}");
        }

        private void NewEbook_Click(object sender, RoutedEventArgs e)
        {
            if (manager == null)
            {
                manager = new ComponentManager();
            }
            
            manager.RunEbook(null);
        }

        private void CloseEbook_Click(object sender, RoutedEventArgs e)
        {
            manager?.Unload();
        }

        private void WriteLine(string msg, ConsoleColor consoleColor = ConsoleColor.Green)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
