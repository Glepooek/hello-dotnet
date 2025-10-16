using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Test.DragControl.Controls
{
    /// <summary>
    /// 自定义TextBox控件
    /// </summary>
    [TemplatePart(Name = ElementAddButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementRemoveButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementMoveUpButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementMoveDownButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementShowButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementPopupToolBar, Type = typeof(Popup))]
    public class SuperTextBox : TextBox
    {
        #region Fields

        private const string ElementAddButton = "PART_AddButton";
        private const string ElementRemoveButton = "PART_RemoveButton";
        private const string ElementMoveUpButton = "PART_MoveUpButton";
        private const string ElementMoveDownButton = "PART_MoveDownButton";
        private const string ElementPlayButton = "PART_PlayButton";
        private const string ElementShowButton = "PART_ShowButton";
        private const string ElementPopupToolBar = "PART_PopupToolBar";

        #endregion

        #region Constructor

        static SuperTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SuperTextBox), new FrameworkPropertyMetadata(typeof(SuperTextBox)));
        }

        #endregion

        #region DependcyProperties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(SuperTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// TextBox类型，仅用于控制底部工具栏区域的显隐
        /// </summary>
        /// <remarks>
        /// 0独白，1对话，2指导语
        /// </remarks>
        public int TextBoxType
        {
            get { return (int)GetValue(TextBoxTypeProperty); }
            set { SetValue(TextBoxTypeProperty, value); }
        }

        public static readonly DependencyProperty TextBoxTypeProperty =
            DependencyProperty.Register(nameof(TextBoxType), typeof(int), typeof(SuperTextBox), new PropertyMetadata(0));

        public bool IsReadOneTime
        {
            get { return (bool)GetValue(IsReadOneTimeProperty); }
            set { SetValue(IsReadOneTimeProperty, value); }
        }

        public static readonly DependencyProperty IsReadOneTimeProperty =
            DependencyProperty.Register(nameof(IsReadOneTime), typeof(bool), typeof(SuperTextBox), new PropertyMetadata(true));

        public bool IsMaleVoice
        {
            get { return (bool)GetValue(IsMaleVoiceProperty); }
            set { SetValue(IsMaleVoiceProperty, value); }
        }

        public static readonly DependencyProperty IsMaleVoiceProperty =
            DependencyProperty.Register(nameof(IsMaleVoice), typeof(bool), typeof(SuperTextBox), new PropertyMetadata(true));

        /// <summary>
        /// 音频当前播放进度
        /// </summary>
        public double AudioProgressValue
        {
            get { return (double)GetValue(AudioProgressValueProperty); }
            set { SetValue(AudioProgressValueProperty, value); }
        }

        public static readonly DependencyProperty AudioProgressValueProperty =
            DependencyProperty.Register(nameof(AudioProgressValue), typeof(double), typeof(SuperTextBox), new PropertyMetadata(0.0));

        /// <summary>
        /// 音频当前播放时长
        /// </summary>
        public TimeSpan AudioCurrentTime
        {
            get { return (TimeSpan)GetValue(AudioCurrentTimeProperty); }
            set { SetValue(AudioCurrentTimeProperty, value); }
        }

        public static readonly DependencyProperty AudioCurrentTimeProperty =
            DependencyProperty.Register(nameof(AudioCurrentTime), typeof(TimeSpan), typeof(SuperTextBox), new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// 音频总时长
        /// </summary>
        public TimeSpan AudioTotalTime
        {
            get { return (TimeSpan)GetValue(AudioTotalTimeProperty); }
            set { SetValue(AudioTotalTimeProperty, value); }
        }

        public static readonly DependencyProperty AudioTotalTimeProperty =
            DependencyProperty.Register(nameof(AudioTotalTime), typeof(TimeSpan), typeof(SuperTextBox), new PropertyMetadata(TimeSpan.Zero));

        #endregion

        #region Commands

        public ICommand PlayCommand
        {
            get { return (ICommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }

        public static readonly DependencyProperty PlayCommandProperty =
            DependencyProperty.Register(nameof(PlayCommand), typeof(ICommand), typeof(SuperTextBox), new PropertyMetadata(null));


        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(SuperTextBox), new PropertyMetadata(null));

        #endregion

        #region Events

        /// <summary>
        /// 新增事件
        /// </summary>
        public event RoutedEventHandler Add
        {
            add { AddHandler(AddEvent, value); }
            remove { RemoveHandler(AddEvent, value); }
        }

        /// <summary>
        /// 新增路由事件
        /// </summary>
        public static readonly RoutedEvent AddEvent = EventManager.RegisterRoutedEvent(
            nameof(Add), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SuperTextBox));

        /// <summary>
        /// 移除事件
        /// </summary>
        public event RoutedEventHandler Remove
        {
            add { AddHandler(RemoveEvent, value); }
            remove { RemoveHandler(RemoveEvent, value); }
        }

        /// <summary>
        /// 移除路由事件
        /// </summary>
        public static readonly RoutedEvent RemoveEvent = EventManager.RegisterRoutedEvent(
            nameof(Remove), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SuperTextBox));

        /// <summary>
        /// 向上移动事件
        /// </summary>
        public event RoutedEventHandler MoveUp
        {
            add { AddHandler(MoveUpEvent, value); }
            remove { RemoveHandler(MoveUpEvent, value); }
        }

        /// <summary>
        /// 向上移动路由事件
        /// </summary>
        public static readonly RoutedEvent MoveUpEvent = EventManager.RegisterRoutedEvent(
            nameof(MoveUp), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SuperTextBox));

        /// <summary>
        /// 向下移动事件
        /// </summary>
        public event RoutedEventHandler MoveDown
        {
            add { AddHandler(MoveDownEvent, value); }
            remove { RemoveHandler(MoveDownEvent, value); }
        }

        /// <summary>
        /// 向下移动路由事件
        /// </summary>
        public static readonly RoutedEvent MoveDownEvent = EventManager.RegisterRoutedEvent(
            nameof(MoveDown), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SuperTextBox));

        /// <summary>
        /// 播放事件
        /// </summary>
        public event RoutedEventHandler Play
        {
            add { AddHandler(PlayEvent, value); }
            remove { RemoveHandler(PlayEvent, value); }
        }

        /// <summary>
        /// 播放路由事件
        /// </summary>
        public static readonly RoutedEvent PlayEvent = EventManager.RegisterRoutedEvent(
            nameof(Play), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SuperTextBox));

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var addButton = GetTemplateChild(ElementAddButton) as Button;
            if (addButton != null)
            {
                addButton.Click += (o, e) =>
                {
                    RaiseEvent(new RoutedEventArgs(AddEvent));
                };
            }

            var removeButton = GetTemplateChild(ElementRemoveButton) as Button;
            if (removeButton != null)
            {
                removeButton.Click += (o, e) =>
                {
                    RaiseEvent(new RoutedEventArgs(RemoveEvent));
                };
            }

            var moveUpButton = GetTemplateChild(ElementMoveUpButton) as Button;
            if (moveUpButton != null)
            {
                moveUpButton.Click += (o, e) =>
                {
                    RaiseEvent(new RoutedEventArgs(MoveUpEvent));
                };
            }

            var moveDownButton = GetTemplateChild(ElementMoveDownButton) as Button;
            if (moveDownButton != null)
            {
                moveDownButton.Click += (o, e) =>
                {
                    RaiseEvent(new RoutedEventArgs(MoveDownEvent));
                };
            }

            var showButton = GetTemplateChild(ElementShowButton) as Button;
            var topToolBar = GetTemplateChild(ElementPopupToolBar) as Popup;
            if (showButton != null && topToolBar != null)
            {
                showButton.Click += (o, e) =>
                {
                    topToolBar.IsOpen = !topToolBar.IsOpen;
                };
                showButton.MouseEnter += (o, e) =>
                {
                    topToolBar.StaysOpen = true;
                };
                showButton.MouseLeave += (o, e) =>
                {
                    topToolBar.StaysOpen = false;
                };
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            CommandManager.InvalidateRequerySuggested();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            CommandManager.InvalidateRequerySuggested();
        }


        #endregion
    }
}
