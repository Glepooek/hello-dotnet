using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using D2D = Microsoft.WindowsAPICodePack.DirectX.Direct2D1;

// 1、WindowsAPICodePackDirectX，该库比较老，且只能运行在X64平台，且不再更新，建议使用SharpDX。
// 2、不能直接在 dot net framework 4.5 的环境运行，需要创建 App.config 文件添加下面代码。
// 需要注意，请修改创建项目使用 dot net framework 4.5 而不是更高的版本。

namespace Learning.Direct2D1.GettingStarted
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRendering;

            Loaded += (s, e) =>
            {
                // 创建工厂
                var d2DFactory = D2D.D2DFactory.CreateFactory(D2D.D2DFactoryType.Multithreaded);
                // 获取窗口句柄
                var windowHandle = new WindowInteropHelper(this).Handle;
                var renderTarget = d2DFactory.CreateHwndRenderTarget(
                    new D2D.RenderTargetProperties(),
                    new D2D.HwndRenderTargetProperties(
                        windowHandle,
                        new D2D.SizeU((uint)ActualWidth, (uint)ActualHeight),
                        D2D.PresentOptions.RetainContents));

                // 红色画刷
                _redBrush = renderTarget.CreateSolidColorBrush(new D2D.ColorF(1, 0, 0, 1));
                // 绿色画刷
                _greenBrush = renderTarget.CreateSolidColorBrush(new D2D.ColorF(0, 1, 0, 1));
                // 蓝色画刷
                _blueBrush = renderTarget.CreateSolidColorBrush(new D2D.ColorF(0, 0, 1, 1));
                _renderTarget = renderTarget;
            };
        }

        private D2D.RenderTarget _renderTarget;
        private D2D.SolidColorBrush _redBrush;
        private D2D.SolidColorBrush _greenBrush;
        private D2D.SolidColorBrush _blueBrush;

        private float _dx = 1;
        private float _dy = 1;
        private float _x;
        private float _y;

        private Random ran = new Random();

        private void OnRendering(object sender, EventArgs e)
        {
            if (_renderTarget == null)
            {
                return;
            }

            D2D.SolidColorBrush brush = null;

            switch (ran.Next(3))
            {
                case 0:
                    brush = _redBrush;
                    break;
                case 1:
                    brush = _greenBrush;
                    break;
                case 2:
                    brush = _blueBrush;
                    break;
            }

            _renderTarget.BeginDraw();
            _renderTarget.DrawRectangle(new D2D.RectF(_x, _y, _x + 10, _y + 10), brush, 1);
            _renderTarget.EndDraw();

            _x += _dx;
            _y += _dy;
            if (_x >= ActualWidth - 100 || _x <= 0)
            {
                _dx = -_dx;
            }

            if (_y >= ActualHeight - 100 || _y <= 0)
            {
                _dy = -_dy;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //MessageBox.Show($"width: {this.ActualWidth}, height: {this.ActualHeight}");
        }
    }
}
