using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace FastScreenCapture.Helpers
{
    public class ImageHelper
    {
        #region Fields

        private static Bitmap m_Bitmap = null;
        private static StringBuilder m_StringBuilder = new StringBuilder();

        #endregion

        #region 保存为PNG图片

        public static void SaveToPng(BitmapSource image, string fileName)
        {
            using (var fs = System.IO.File.Create(fileName))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fs);
            }
        }

        #endregion

        #region 截图

        public static BitmapSource GetBitmapSource(int x, int y, int width, int height)
        {
            var bounds = ScreenHelper.GetPhysicalDisplaySize();
            var screenWidth = bounds.Width;
            var screenHeight = bounds.Height;
            var scaleWidth = (screenWidth * 1.0) / SystemParameters.PrimaryScreenWidth;
            var scaleHeight = (screenHeight * 1.0) / SystemParameters.PrimaryScreenHeight;
            var w = (int)(width * scaleWidth);
            var h = (int)(height * scaleHeight);
            var l = (int)(x * scaleWidth);
            var t = (int)(y * scaleHeight);
            using (var bm = new Bitmap(w, h, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(bm))
                {
                    g.CopyFromScreen(l, t, 0, 0, bm.Size);
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        bm.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        #endregion

        #region 全屏截图

        public static BitmapSource GetFullBitmapSource()
        {
            var bounds = ScreenHelper.GetPhysicalDisplaySize();
            m_Bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            var bmpGraphics = Graphics.FromImage(m_Bitmap);
            bmpGraphics.CopyFromScreen(0, 0, 0, 0, m_Bitmap.Size);
            return Imaging.CreateBitmapSourceFromHBitmap(
                m_Bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        #endregion

        #region 获取RGB

        /// <summary>
        /// 获取图片的RGB值
        /// </summary>
        /// <param name="x">鼠标光标位置X轴坐标</param>
        /// <param name="y">鼠标光标位置Y轴坐标</param>
        /// <returns></returns>
        public static string GetImageRGB(int x, int y)
        {
            var color = m_Bitmap.GetPixel(x, y);
            m_StringBuilder.Clear();
            m_StringBuilder.Append("RGB:（");
            m_StringBuilder.Append(color.R.ToString());
            m_StringBuilder.Append(",");
            m_StringBuilder.Append(color.G.ToString());
            m_StringBuilder.Append(",");
            m_StringBuilder.Append(color.B.ToString());
            m_StringBuilder.Append("）");
            return m_StringBuilder.ToString();
        }

        #endregion
    }
}
