using SharpDX;
using SharpDX.Mathematics.Interop;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using D2D = SharpDX.Direct2D1;
using DW = SharpDX.DirectWrite;
using DXGI = SharpDX.DXGI;

namespace Learning.SharpDX.DrawTexts
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DW.Factory _factory;
        private D2D.RenderTarget _renderTarget;

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRendering;

            this.Loaded += (o, e) =>
             {
                 var factory = new D2D.Factory();
                 _factory = new DW.Factory();
                 var pixelFormat = new D2D.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, D2D.AlphaMode.Premultiplied);

                 var renderTargetProperties = new D2D.RenderTargetProperties(
                        D2D.RenderTargetType.Default,
                        pixelFormat,
                        96,
                        96,
                        D2D.RenderTargetUsage.None,
                        D2D.FeatureLevel.Level_DEFAULT);

                 var hwndRenderTargetProperties = new D2D.HwndRenderTargetProperties
                 {
                     Hwnd = new WindowInteropHelper(this).Handle,
                     PixelSize = new Size2((int)ActualWidth, (int)ActualHeight),
                     PresentOptions = D2D.PresentOptions.RetainContents
                 };

                 _renderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
             };

        }

        private void OnRendering(object sender, EventArgs e)
        {
            if (_renderTarget == null)
            {
                return;
            }

            var brush = new D2D.SolidColorBrush(_renderTarget, new RawColor4(1, 0, 0, 1));
            _renderTarget.BeginDraw();
            _renderTarget.DrawText("lindexi 本文所有博客放在 lindexi.oschina.io \n欢迎大家来访问\n\n这是系列博客，告诉大家如何在 WPF 使用Direct2D1", new DW.TextFormat(_factory, "宋体", 20), new RawRectangleF(10, 10, 1000, 1000), brush);
            _renderTarget.EndDraw();
        }
    }
}
