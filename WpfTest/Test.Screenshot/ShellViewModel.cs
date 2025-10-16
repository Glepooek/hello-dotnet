using Stylet;
using System;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Test.Screenshot.Win32Api;

namespace Test.Screenshot
{
    public class ShellViewModel : Screen
    {
        #region Binding Properties

        private BitmapSource mImageSource;
        public BitmapSource ImageSource
        {
            get => mImageSource;
            set => SetAndNotify(ref mImageSource, value);
        }

        #endregion

        #region Binding Methods

        public void DoScreenshot()
        {
            string fileName = $"{AppDomain.CurrentDomain.BaseDirectory}{DateTime.Now.ToUnixTime()}.jpg";
            if (View is ShellView shell)
            {
                IntPtr handle = new WindowInteropHelper(shell).EnsureHandle();
                // 这样直接赋值图片不显示
                // 返回的数据的实际类型是System.Windows.Interop.InteropBitmap
                //ImageSource = GetWindowBitmap(handle);
                BitmapSource bitmap = GetWindowBitmap(handle);
                SaveImageToFile(bitmap, fileName);
                ImageSource = LoadImageFromFile(fileName);

                //Capture(shell, fileName);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 保存图片到文件
        /// </summary>
        /// <param name="image">图片数据</param>
        /// <param name="filePath">保存路径</param>
        private void SaveImageToFile(BitmapSource image, string filePath)
        {
            if (image == null || string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            BitmapEncoder encoder = GetBitmapEncoder(filePath);
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(stream);
                stream.Flush();
            }
        }

        /// <summary>
        /// 获取Bitmap编码器
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private BitmapEncoder GetBitmapEncoder(string filePath)
        {
            string extName = Path.GetExtension(filePath).ToLower();
            BitmapEncoder encoder = extName switch
            {
                "png" => new PngBitmapEncoder(),
                "gif" => new GifBitmapEncoder(),
                _ => new JpegBitmapEncoder() { QualityLevel = 100 },
            };
            return encoder;
        }

        /// <summary>
        /// 从文件加载图片
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        private BitmapImage LoadImageFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(filePath)))
            {
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

        /// <summary>
        /// 根据窗口句柄获取截图
        /// </summary>
        /// <param name="handle">窗口句柄</param>
        /// <returns>截图</returns>
        public BitmapSource GetWindowBitmap(IntPtr handle)
        {
            if (GetClientRect(handle, out RECT rect) == 0)
            {
                return null;
            }

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            if (width <= 0 || height <= 0)
            {
                return null;
            }

            return Capture(handle, rect.left, rect.top, width, height);
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="handle">窗口句柄</param>
        /// <param name="xSrc">窗口左上角x坐标</param>
        /// <param name="ySrc">窗口左上角y坐标</param>
        /// <param name="width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        /// <returns></returns>
        private BitmapSource Capture(IntPtr handle, int xSrc, int ySrc, int width, int height)
        {
            IntPtr hdc = handle;
            IntPtr screenDC = GetDC(hdc);
            IntPtr compatibleDC = CreateCompatibleDC(screenDC);
            IntPtr bmp = CreateCompatibleBitmap(screenDC, width, height);

            try
            {
                SelectObject(compatibleDC, bmp);
                BitBlt(compatibleDC, 0, 0, width, height, screenDC, xSrc, ySrc, 0x00CC0020);

                BitmapSource image = Imaging.CreateBitmapSourceFromHBitmap(bmp, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                image.Freeze();
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                DeleteObject(bmp);
                DeleteDC(compatibleDC);
                ReleaseDC(hdc, screenDC);
            }
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="target">可视化元素</param>
        /// <param name="fileName">保存路径</param>
        private void Capture(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            var targetBitmap = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Default);
            targetBitmap.Render(target);

            try
            {
                BitmapEncoder encoder = GetBitmapEncoder(fileName);
                encoder.Frames.Add(BitmapFrame.Create(targetBitmap));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    encoder.Save(fs);
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Capture1(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }

            renderTarget.Render(visual);

            BitmapEncoder encoder = GetBitmapEncoder(fileName);
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));
            using (Stream stm = File.Create(fileName))
            {
                encoder.Save(stm);
                stm.Flush();
            }
        }
        #endregion
    }
}
