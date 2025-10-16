using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test.Multilanguage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //这里测试可以看到，凡是标记为StringResourceExtension XAML对象，该对象的Language_Event都会添加到LanguageChangedEvent事件
            int count = GlobalConfig.LanguageChangedEvent.GetInvocationList().Count();
            Console.WriteLine(count);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GlobalConfig.ChangeLanguage("zh-CN");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GlobalConfig.ChangeLanguage("en-US");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(GlobalConfig.GetString("toast"));
        }
    }
}
