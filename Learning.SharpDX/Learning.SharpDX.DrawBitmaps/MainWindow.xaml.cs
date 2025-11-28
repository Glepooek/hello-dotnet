using SharpDX;
using SharpDX.IO;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using D2D = SharpDX.Direct2D1;
using DXGI = SharpDX.DXGI;
using WIC = SharpDX.WIC;

namespace Learning.SharpDX.DrawBitmaps
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private D2D.RenderTarget mRenderTarget;
        private string mFilename = AppDomain.CurrentDomain.BaseDirectory + "file.jpg";

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRendering;

            this.Loaded += (o, e) =>
            {
                var factory = new D2D.Factory();
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

                mRenderTarget = new D2D.WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);
            };
        }

        private void OnRendering(object sender, EventArgs e)
        {
            if (mRenderTarget == null)
            {
                return;
            }

            // 加载位图
            D2D.Bitmap bitmap = LoadBitmapFromContentFile(mFilename);
            // 绘制位图
            mRenderTarget.BeginDraw();
            mRenderTarget.DrawBitmap(bitmap, 1, D2D.BitmapInterpolationMode.Linear);
            mRenderTarget.EndDraw();
        }

        /// <summary>
        /// WIC加载位图并转换成D2D位图
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private D2D.Bitmap LoadBitmapFromContentFile(string filePath)
        {
            WIC.ImagingFactory imagingFactory = new WIC.ImagingFactory();
            NativeFileStream fileStream = new NativeFileStream(filePath,
                NativeFileMode.Open, NativeFileAccess.Read);

            WIC.BitmapDecoder bitmapDecoder = new WIC.BitmapDecoder(imagingFactory, fileStream, WIC.DecodeOptions.CacheOnLoad);

            using (imagingFactory)
            using (fileStream)
            using (bitmapDecoder)
            {
                WIC.BitmapFrameDecode frame = bitmapDecoder.GetFrame(0);

                using (frame)
                {
                    WIC.FormatConverter converter = new WIC.FormatConverter(imagingFactory);
                    using (converter)
                    {
                        converter.Initialize(frame, WIC.PixelFormat.Format32bppPRGBA);
                        var bitmap = D2D.Bitmap.FromWicBitmap(mRenderTarget, converter);

                        return bitmap;
                    }
                }
            }
        }
    }
}
