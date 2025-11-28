using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FastScreenCapture.Controls
{
    /// <summary>
    /// TextBox外部被border包裹的控件
    /// </summary>
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class TextBoxControl : Control
    {
        #region Fields

        private TextBox _TextBox;

        #endregion

        #region BorderColor DependencyProperty

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
                DependencyProperty.Register(nameof(BorderColor), typeof(Color), typeof(TextBoxControl),
                new PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(TextBoxControl.OnBorderColorPropertyChanged)));

        private static void OnBorderColorPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is TextBoxControl)
            {
                (obj as TextBoxControl).OnBorderColorValueChanged();
            }
        }

        protected void OnBorderColorValueChanged()
        {

        }

        #endregion

        #region MyFocus DependencyProperty

        public bool MyFocus
        {
            get { return (bool)GetValue(MyFocusProperty); }
            set { SetValue(MyFocusProperty, value); }
        }
        public static readonly DependencyProperty MyFocusProperty =
                DependencyProperty.Register(nameof(MyFocus), typeof(bool), typeof(TextBoxControl),
                new PropertyMetadata(true, new PropertyChangedCallback(TextBoxControl.OnMyFocusPropertyChanged)));

        private static void OnMyFocusPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is TextBoxControl)
            {
                (obj as TextBoxControl).OnMyFocusValueChanged();
            }
        }

        protected void OnMyFocusValueChanged()
        {
            if (!MyFocus)
            {
                if (string.IsNullOrEmpty(_TextBox.Text))
                {
                    ScreenCaptureWindow.RemoveControl(this);
                }
                else
                {
                    ScreenCaptureWindow.Current.MainImage.ResetLimit(Canvas.GetLeft(this), Canvas.GetTop(this), (Canvas.GetLeft(this) + ActualWidth), (Canvas.GetTop(this) + ActualHeight));
                    ScreenCaptureWindow.Register(this);
                }
                ScreenCaptureWindow.Current.MainImage.m_TextBoxControl = null;
            }
        }

        #endregion

        #region Constructor

        static TextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxControl), new FrameworkPropertyMetadata(typeof(TextBoxControl)));
        }

        public TextBoxControl()
        {
            AddHandler(GotFocusEvent, new RoutedEventHandler((sender, e) =>
            {
                MyFocus = true;
            }));
            AddHandler(LostFocusEvent, new RoutedEventHandler((sender, e) =>
            {
                MyFocus = false;
            }));
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _TextBox = GetTemplateChild("PART_TextBox") as TextBox;
            _TextBox.MaxWidth = ScreenCaptureWindow.Current.MainImage.Width - ScreenCaptureWindow.Current.MainImage.point.X - 3;
            _TextBox.MaxHeight = ScreenCaptureWindow.Current.MainImage.Height - ScreenCaptureWindow.Current.MainImage.point.Y - 3;
        }

        #endregion
    }
}
