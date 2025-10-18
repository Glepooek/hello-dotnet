using CommunityToolkit.Mvvm.DependencyInjection;
using IocSamples.Services;
using IocSamples.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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

namespace IocSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        //private MainViewModel viewModel;
        public MainView()
        {
            InitializeComponent();

            //IFilesService fileService = App.Current.Services.GetService<IFilesService>();
            //MessageBox.Show(fileService.GetFile("test"));

            //viewModel = App.Current.Services.GetService<MainViewModel>();
            //this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ioc.Default.GetService<ILogger>().Information("call Button_Click");
            MessageBox.Show(Ioc.Default.GetService<MainViewModel>().FileName);
        }
    }
}
