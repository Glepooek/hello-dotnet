using SharpDX;
using SharpDX.Mathematics.Interop;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using D2D = SharpDX.Direct2D1;
using DXGI = SharpDX.DXGI;

namespace Learning.SharpDX.GettingStarted
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private D2D.RenderTarget _renderTarget;
        private D2D.Factory _factory;

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRendering;

            Loaded += (s, e) =>
            {
                var factory = new D2D.Factory();

                /***
                 * PixelFormat 使用 B8G8R8A8_UNorm 的意思是每个元素包含4个8位无符号分量，分量的取值范围在[0,1]区间内的浮点数，因为不是任何类型的数据都能存储到纹理中的，纹理只支持特定格式的数据存储。这里的 BGRA 的意思分别是 蓝色（Blue）、绿色（Green）、红色（Red）和 alpha（透明度），其他可以选的格式
                 * DXGI_FORMAT_R32G32B32_FLOAT：每个元素包含3个32位浮点分量。
                 * DXGI_FORMAT_R16G16B16A16_UNORM：每个元素包含4个16位分量，分量的取值范围在[0,1]区间内。
                 * DXGI_FORMAT_R32G32_UINT：每个元素包含两个32位无符号整数分量。
                 * DXGI_FORMAT_R8G8B8A8_UNORM：每个元素包含4个8位无符号分量，分量的取值范围在[0,1]区间内的浮点数。
                 * DXGI_FORMAT_R8G8B8A8_SNORM：每个元素包含4个8位有符号分量，分量的取值范围在[−1,1] 区间内的浮点数。
                 * DXGI_FORMAT_R8G8B8A8_SINT：每个元素包含4个8位有符号整数分量，分量的取值范围在[−128, 127] 区间内的整数。
                 * DXGI_FORMAT_R8G8B8A8_UINT：每个元素包含4个8位无符号整数分量，分量的取值范围在[0, 255]区间内的整数
                 ***/
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
                    // 呈现目标不会再呈现时丢弃帧
                    PresentOptions = D2D.PresentOptions.RetainContents
                };

                _renderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
                _factory = factory;
            };
        }

        private void OnRendering(object sender, EventArgs e)
        {
            if (_renderTarget == null)
            {
                return;
            }

            var ellipse = new D2D.Ellipse(new RawVector2(100, 100), 10, 50);
            var ellipse1 = new D2D.Ellipse(new RawVector2(100, 100), 50, 50);
            // 画刷
            var brush = new D2D.SolidColorBrush(_renderTarget, new RawColor4(1, 0, 0, 1));
            // 设置笔画样式
            var strokeStyleProperties = new D2D.StrokeStyleProperties
            {
                StartCap = D2D.CapStyle.Round,
                EndCap = D2D.CapStyle.Round,
                DashStyle = D2D.DashStyle.DashDot,
                DashCap = D2D.CapStyle.Square,
                DashOffset = 2
            };

            _renderTarget.BeginDraw();
            // 绘制直线
            _renderTarget.DrawLine(new RawVector2(10, 10), new RawVector2(100, 20), brush, 2, new D2D.StrokeStyle(_factory, strokeStyleProperties));
            // 绘制矩形
            _renderTarget.DrawRectangle(new RawRectangleF(100, 100, 200, 200), brush, 2, new D2D.StrokeStyle(_factory, strokeStyleProperties));
            // 绘制圆角矩形
            _renderTarget.DrawRoundedRectangle(new D2D.RoundedRectangle() { Rect = new RawRectangleF(100, 205, 200, 300), RadiusX = 5, RadiusY = 15 }, brush, 2, new D2D.StrokeStyle(_factory, strokeStyleProperties));
            // 绘制椭圆
            _renderTarget.DrawEllipse(ellipse, brush, 1);
            // 绘制圆
            _renderTarget.DrawEllipse(ellipse1, brush, 1);
            // 绘制三角形，画三条连接在一起的直线

            _renderTarget.EndDraw();
        }
    }
}
