using FastScreenCapture.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FastScreenCapture.Controls
{
    public class ToolButton : RadioButton
    {
        #region Tool DependencyProperty

        public Tool Tool
        {
            get { return (Tool)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
        }
        public static readonly DependencyProperty ToolProperty =
                DependencyProperty.Register(nameof(Tool), typeof(Tool), typeof(ToolButton),
                new PropertyMetadata(Tool.Null, new PropertyChangedCallback(ToolButton.OnToolPropertyChanged)));

        private static void OnToolPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ToolButton)
            {
                (obj as ToolButton).OnToolValueChanged();
            }
        }

        protected void OnToolValueChanged()
        {

        }

        #endregion

        #region IsImageEditBar DependencyProperty

        public bool IsImageEditBar
        {
            get { return (bool)GetValue(IsImageEditBarProperty); }
            set { SetValue(IsImageEditBarProperty, value); }
        }
        public static readonly DependencyProperty IsImageEditBarProperty =
                DependencyProperty.Register(nameof(IsImageEditBar), typeof(bool), typeof(ToolButton),
                new PropertyMetadata(false, new PropertyChangedCallback(ToolButton.OnIsImageEditBarPropertyChanged)));

        private static void OnIsImageEditBarPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ToolButton)
            {
                (obj as ToolButton).OnIsImageEditBarValueChanged();
            }
        }

        protected void OnIsImageEditBarValueChanged()
        {

        }

        #endregion

        #region LineColor DependencyProperty

        public SolidColorBrush LineColor
        {
            get { return (SolidColorBrush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        public static readonly DependencyProperty LineColorProperty =
                DependencyProperty.Register(nameof(LineColor), typeof(SolidColorBrush), typeof(ToolButton),
                new PropertyMetadata(null, new PropertyChangedCallback(ToolButton.OnLineColorPropertyChanged)));

        private static void OnLineColorPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ToolButton)
            {
                (obj as ToolButton).OnLineColorValueChanged();
            }
        }

        protected void OnLineColorValueChanged()
        {

        }

        #endregion

        #region Constructor

        static ToolButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolButton), new FrameworkPropertyMetadata(typeof(ToolButton)));
        }

        #endregion

        #region 覆写点击事件（可弹起）

        protected override void OnClick()
        {
            if (IsImageEditBar)
            {
                switch (Tool)
                {
                    case Tool.Revoke:
                        ScreenCaptureWindow.Current.OnRevoke();
                        break;
                    case Tool.Save:
                        ScreenCaptureWindow.Current.OnSave();
                        break;
                    case Tool.Cancel:
                        ScreenCaptureWindow.Current.OnCancel();
                        break;
                    case Tool.OK:
                        ScreenCaptureWindow.Current.OnOK();
                        break;
                    default:
                        IsChecked = !IsChecked;
                        SizeColorBar.Current.Selected = IsChecked == true ? Tool : Tool.Null;
                        break;
                }
            }
            else
            {
                if (IsChecked == false)
                {
                    IsChecked = true;
                    if (LineColor != null)
                    {
                        SizeColorBar.Current.ChangeColor(LineColor);
                    }
                }
            }
        }

        #endregion
    }
}
