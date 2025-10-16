using System.Windows;
using System.Windows.Data;
using Test.ListViewDemo.ViewModels;

namespace Test.ListViewDemo.Views
{
    /// <summary>
    /// UsingSortAndGroup.xaml 的交互逻辑
    /// </summary>
    public partial class UsingSortAndGroup : Window
    {
        public UsingSortAndGroup()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e) { }
    }
}
