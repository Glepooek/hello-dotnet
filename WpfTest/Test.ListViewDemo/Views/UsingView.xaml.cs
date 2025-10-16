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
using System.Windows.Shapes;
using Test.ListViewDemo.ViewModels;

namespace Test.ListViewDemo.Views
{
    /// <summary>
    /// UsingView.xaml 的交互逻辑
    /// </summary>
    public partial class UsingView : Window
    {
        public UsingView()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }
    }
}
