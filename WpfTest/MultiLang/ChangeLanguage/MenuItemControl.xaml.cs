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
using LanguageResource;

namespace ChangeLanguage
{
    /// <summary>
    /// Interaction logic for MenuItemControl.xaml
    /// </summary>
    public partial class MenuItemControl : UserControl
    {
        public MenuItemControl()
        {
            InitializeComponent();
        }

        // Change language.
        private void menuHelp2_Click(object sender, RoutedEventArgs e)
        {
            var model = (LanguageSelectionViewModel)this.FindResource("LanguageSelectionViewModel");
            
            MenuItem item = e.OriginalSource as MenuItem;

            if (model.ChangeLanguage(item.Header.ToString()))
            {
                menuHelp2.Items.Refresh();
            }
        }
    }
}
