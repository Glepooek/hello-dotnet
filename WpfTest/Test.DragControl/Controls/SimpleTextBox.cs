using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Test.DragControl.Controls
{
    [TemplatePart(Name = ElementAddButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementRemoveButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementMoveUpButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementMoveDownButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementShowButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementPopupToolBar, Type = typeof(Popup))]
    public class SimpleTextBox : TextBox
    {
        #region Fields

        private const string ElementAddButton = "PART_AddButton";
        private const string ElementRemoveButton = "PART_RemoveButton";
        private const string ElementMoveUpButton = "PART_MoveUpButton";
        private const string ElementMoveDownButton = "PART_MoveDownButton";
        private const string ElementShowButton = "PART_ShowButton";
        private const string ElementPopupToolBar = "PART_PopupToolBar";

        #endregion

        #region Constructor

        static SimpleTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleTextBox), new FrameworkPropertyMetadata(typeof(SimpleTextBox)));
        }

        public SimpleTextBox()
        {
            // 禁用粘贴
            this.CommandBindings.Add(new CommandBinding
                (ApplicationCommands.Paste, (sender, args) =>
            {
                args.Handled = true;
            }));

            // 禁用复制
            this.CommandBindings.Add(new CommandBinding
                (ApplicationCommands.Copy, (sender, args) =>
            {
                args.Handled = true;
            }));

            // 禁用剪切
            this.CommandBindings.Add(new CommandBinding
                (ApplicationCommands.Cut, (sender, args) =>
            {
                args.Handled = true;
            }));
        }

        #endregion

        #region DependcyProperties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(SimpleTextBox), new PropertyMetadata(string.Empty));


        public int PauseTime
        {
            get { return (int)GetValue(PauseTimeProperty); }
            set { SetValue(PauseTimeProperty, value); }
        }

        public static readonly DependencyProperty PauseTimeProperty =
            DependencyProperty.Register(nameof(PauseTime), typeof(int), typeof(SimpleTextBox), new PropertyMetadata(0));

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
            nameof(Add), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SimpleTextBox));

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
            nameof(Remove), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SimpleTextBox));

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
            nameof(MoveUp), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SimpleTextBox));

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
            nameof(MoveDown), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SimpleTextBox));

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
            nameof(Play), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SimpleTextBox));

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

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}
