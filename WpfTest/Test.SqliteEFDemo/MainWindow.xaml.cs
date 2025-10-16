using System.Windows;

namespace Test.SqliteEFDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mMainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            //SQLiteHelper.Instance.DeleteAllAsync<Stock>();
            mMainViewModel = new MainViewModel();
            this.DataContext = mMainViewModel;
            this.Loaded += (sender, args) =>
            {
                mMainViewModel.GetStockList();
            };
        }
    }
}
