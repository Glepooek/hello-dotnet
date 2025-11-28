using FastScreenCapture.Models;
using System.Windows;
using System.Windows.Controls;

namespace FastScreenCapture.Controls
{
    public class ImageEditBar : Control
    {
        #region 属性 Current

        private static ImageEditBar _Current = null;
        public static ImageEditBar Current
        {
            get
            {
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }

        #endregion

        #region CanvasLeft DependencyProperty

        public double CanvasLeft
        {
            get { return (double)GetValue(CanvasLeftProperty); }
            set { SetValue(CanvasLeftProperty, value); }
        }
        public static readonly DependencyProperty CanvasLeftProperty =
                DependencyProperty.Register(nameof(CanvasLeft), typeof(double), typeof(ImageEditBar),
                new PropertyMetadata(0.0, new PropertyChangedCallback(ImageEditBar.OnCanvasLeftPropertyChanged)));

        private static void OnCanvasLeftPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ImageEditBar)
            {
                (obj as ImageEditBar).OnCanvasLeftValueChanged();
            }
        }

        protected void OnCanvasLeftValueChanged()
        {

        }

        private void ResetCanvasLeft()
        {
            CanvasLeft = AppModel.Current.MaskRightWidth > ScreenCaptureWindow.ScreenWidth - Width ? 0 : ScreenCaptureWindow.ScreenWidth - AppModel.Current.MaskRightWidth - Width;
        }

        #endregion

        #region CanvasTop DependencyProperty

        public double CanvasTop
        {
            get { return (double)GetValue(CanvasTopProperty); }
            set { SetValue(CanvasTopProperty, value); }
        }
        public static readonly DependencyProperty CanvasTopProperty =
                DependencyProperty.Register(nameof(CanvasTop), typeof(double), typeof(ImageEditBar),
                new PropertyMetadata(0.0, new PropertyChangedCallback(ImageEditBar.OnCanvasTopPropertyChanged)));

        private static void OnCanvasTopPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ImageEditBar)
            {
                (obj as ImageEditBar).OnCanvasTopValueChanged();
            }
        }

        protected void OnCanvasTopValueChanged()
        {

        }

        private void ResetCanvasTop()
        {
            var need = 30 + (SizeColorBar.Current.Selected == Tool.Null ? 0 : 37);
            CanvasTop = AppModel.Current.MaskBottomHeight >= need ? ScreenCaptureWindow.ScreenHeight - AppModel.Current.MaskBottomHeight + 5
                : AppModel.Current.MaskTopHeight >= need ? AppModel.Current.MaskTopHeight - 30
                : AppModel.Current.MaskTopHeight;
        }

        #endregion

        #region Constructor

        static ImageEditBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageEditBar), new FrameworkPropertyMetadata(typeof(ImageEditBar)));
        }

        public ImageEditBar()
        {
            _Current = this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 重新定位控件位置
        /// </summary>
        public void ResetCanvas()
        {
            ResetCanvasLeft();
            ResetCanvasTop();
        }

        #endregion
    }
}
