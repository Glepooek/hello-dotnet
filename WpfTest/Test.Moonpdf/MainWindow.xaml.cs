using System;
using System.Windows;
using System.Windows.Controls;

namespace Test.Moonpdf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isLoaded = false;
        private string appBasePath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            appBasePath = AppDomain.CurrentDomain.BaseDirectory;

            this.Loaded += (o, e) =>
             {
                 string filePath = $"{appBasePath}Documents\\爱学习隐私权政策（20210318-终版-清洁版）.pdf";
                 moonPdfPanel.OpenFile(filePath);
                 _isLoaded = true;
                 tabControl.SelectionChanged += TabControl_SelectionChanged;
             };
        }

        //private void FileButton_Click(object sender, RoutedEventArgs e)
        //{
        //	var dialog = new OpenFileDialog();

        //	if (dialog.ShowDialog().GetValueOrDefault())
        //	{
        //		string filePath = dialog.FileName;

        //		try
        //		{
        //			moonPdfPanel.OpenFile(filePath);
        //			_isLoaded = true;
        //		}
        //		catch (Exception)
        //		{
        //			_isLoaded = false;
        //		}
        //	}
        //}

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var tabControl = sender as TabControl;
            _isLoaded = false;

            if (tabControl.SelectedIndex == 0)
            {
                string filePath = $"{appBasePath}Documents\\爱学习隐私权政策（20210318-终版-清洁版）.pdf";
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
            else
            {
                string filePath = $"{appBasePath}Documents\\爱学习用户服务协议（20210318-终版-清洁版）.pdf";
                moonPdfPanel.OpenFile(filePath);
                _isLoaded = true;
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomIn();
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomOut();
            }
        }

        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.Zoom(1.0);
            }
        }

        private void FitToHeightButton_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.ZoomToHeight();
        }

        private void FacingButton_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.ViewType = MoonPdfLib.ViewType.Facing;
        }

        private void SinglePageButton_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.ViewType = MoonPdfLib.ViewType.SinglePage;
        }
    }
}
