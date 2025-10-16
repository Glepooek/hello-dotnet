using System;
using System.Windows;
using System.Windows.Controls;

namespace Test.ListBoxPage
{
    /// <summary>
    /// ListBoxPage.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxPage : ListBox
    {
        TextBlock Loading;
        /// <summary>
        /// 是否在loading中
        /// </summary>  
        bool IsLoading;

        #region DependencyProperty

        /// <summary>
        /// 滚动条距离触发加载的高度线
        /// </summary>
        public int Loadline
        {
            get { return (int)GetValue(LoadlineProperty); }
            set { SetValue(LoadlineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Loadline.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadlineProperty =
            DependencyProperty.Register("Loadline", typeof(int), typeof(ListBoxPage), new PropertyMetadata(100));


        /// <summary>
        /// 是否结束数据加载(主动设置并触发加载完成事件)
        /// </summary>
        public int LoadEnd
        {
            get { return (int)GetValue(LoadEndProperty); }
            set { SetValue(LoadEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadEndProperty =
            DependencyProperty.Register("LoadEnd", typeof(int), typeof(ListBoxPage), new PropertyMetadata(0, (d, e) =>
            {
                if (e.NewValue == null) return;

                if (d is ListBoxPage page)
                {
                    page.EndLoading();
                }
            }));


        /// <summary>
        /// 是否完成加载(为true时,不会再触发BeginLoadEvent)
        /// </summary>
        public bool Completed
        {
            get { return (bool)GetValue(CompletedProperty); }
            set { SetValue(CompletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Completed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompletedProperty =
            DependencyProperty.Register("Completed", typeof(bool), typeof(ListBoxPage), new PropertyMetadata(false));

        #endregion

        #region Event

        /// <summary>
        /// 开始加载事件
        /// </summary>
        public static readonly RoutedEvent BeginLoadEvent
            = EventManager.RegisterRoutedEvent(nameof(BeginLoad), RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ListBoxPage));

        public event RoutedEventHandler BeginLoad
        {
            add { AddHandler(BeginLoadEvent, value); }
            remove { RemoveHandler(BeginLoadEvent, value); }
        }

        /// <summary>
        /// 结束加载事件
        /// </summary>
        public static readonly RoutedEvent EndLoadEvent
            = EventManager.RegisterRoutedEvent(nameof(EndLoad), RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ListBoxPage));

        public event RoutedEventHandler EndLoad
        {
            add { AddHandler(EndLoadEvent, value); }
            remove { RemoveHandler(EndLoadEvent, value); }
        }

        #endregion


        public ListBoxPage()
        {
            InitializeComponent();
            this.Loaded += ListBoxPage_Loaded;
        }

        private void ListBoxPage_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer scroll = (ScrollViewer)this.Template.FindName("scroll", this);
            Loading = (TextBlock)this.Template.FindName("loading", this);

            if (scroll == null)
            {
                return;
            }
            scroll.ScrollChanged += Scroll_ScrollChanged;
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (Completed) return;

            if (e.VerticalOffset > 0 && e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight - Loadline)
            {
                BeginLoading();
            }
        }

        /// <summary>
        /// 开始加载
        /// </summary>
        private void BeginLoading()
        {
            if (IsLoading)
            {
                return;
            }
            IsLoading = true;
            this.RaiseEvent(new RoutedEventArgs(BeginLoadEvent, this));

            if (Loading != null)
            {
                Loading.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 结束加载
        /// </summary>
        private void EndLoading()
        {
            IsLoading = false;
            this.RaiseEvent(new RoutedEventArgs(EndLoadEvent, this));
            if (Loading != null)
            {
                Loading.Visibility = Visibility.Collapsed;
            }
        }
    }
}
