using Microsoft.WindowsAPICodePack.DirectX.DirectWrite;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using D2D = Microsoft.WindowsAPICodePack.DirectX.Direct2D1;

/***
 * 点：Point2F
 * 线段：DrawLine()
 * 矩形
 * 椭圆
 * 文本
 * ***/

namespace Learning.Direct2D1.DrawGraphics
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
                // 绘制图形需要的工厂
                _d2DFactory = D2D.D2DFactory.CreateFactory(D2D.D2DFactoryType.Multithreaded);
                // 绘制文本需要用到的工厂
                _dWriteFactory = DWriteFactory.CreateFactory();
                _textFormat = _dWriteFactory.CreateTextFormat("宋体", 20);

                // 获取窗口句柄
                var windowHandle = new WindowInteropHelper(this).Handle;
                var renderTarget = _d2DFactory.CreateHwndRenderTarget(
                    new D2D.RenderTargetProperties(),
                    new D2D.HwndRenderTargetProperties(
                        windowHandle,
                        new D2D.SizeU((uint)ActualWidth, (uint)ActualHeight),
                        D2D.PresentOptions.RetainContents));

                _redBrush = renderTarget.CreateSolidColorBrush(new D2D.ColorF(1, 0, 0, 1));
                _greenBrush = renderTarget.CreateSolidColorBrush(new D2D.ColorF(0, 1, 0, 1));
                _blueBrush = renderTarget.CreateSolidColorBrush(new D2D.ColorF(0, 0, 1, 1));
                _renderTarget = renderTarget;
            };
        }

        private D2D.RenderTarget _renderTarget;
        private D2D.D2DFactory _d2DFactory;
        private DWriteFactory _dWriteFactory;
        private TextFormat _textFormat;
        private D2D.SolidColorBrush _redBrush;
        private D2D.SolidColorBrush _greenBrush;
        private D2D.SolidColorBrush _blueBrush;

        private void OnRendering(object sender, EventArgs e)
        {
            if (_renderTarget == null)
            {
                return;
            }

            var strokeStyleProperties = new D2D.StrokeStyleProperties();
            strokeStyleProperties.StartCap = D2D.CapStyle.Round;
            strokeStyleProperties.EndCap = D2D.CapStyle.Round;
            strokeStyleProperties.DashStyle = D2D.DashStyle.DashDot;
            strokeStyleProperties.DashCap = D2D.CapStyle.Square;
            strokeStyleProperties.DashOffset = 2;

            var strokeStyle = _d2DFactory.CreateStrokeStyle(strokeStyleProperties);

            _renderTarget.BeginDraw();
            // 画线段：起始点、结束点、画刷、线宽、笔画样式
            _renderTarget.DrawLine(new D2D.Point2F(10, height), new D2D.Point2F(100, height), _redBrush, 1, strokeStyle);
            // 绘制文本：内容、文本格式、位置、画刷
            _renderTarget.DrawText("lindexi 本文所有博客放在 lindexi.oschina.io \n欢迎大家来访问\n\n这是系列博客，告诉大家如何在 WPF 使用Direct2D1", _textFormat, new D2D.RectF(10, 10, 1000, 1000), _greenBrush);
            _renderTarget.EndDraw();
        }

        float height = 10;
    }
}
