using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Test.CountDown
{
    /// <summary>
    /// 倒计时控件
    /// </summary>
    public class CountDownControl : Control
    {
        #region Constructor

        static CountDownControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CountDownControl), new FrameworkPropertyMetadata(typeof(CountDownControl)));
        }

        public CountDownControl()
        {
            mDispatcherTimer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };

            Loaded += CountDownControl_Loaded;
            Unloaded += CountDownControl_Unloaded;
        }

        ~CountDownControl() => Dispose();

        #endregion

        #region Fields

        private readonly DispatcherTimer mDispatcherTimer;
        private bool mIsDisposed;
        private int mMinute;
        private int mSecond;

        #endregion

        #region DependcyProperties

        public static readonly DependencyProperty NumberListProperty = DependencyProperty.Register(
            nameof(NumberList), typeof(List<int>), typeof(CountDownControl), new PropertyMetadata(new List<int> { 0, 0, 0, 0 }));

        /// <summary>
        /// 分钟十位、个位和秒十位、个位数集合
        /// </summary>
        public List<int> NumberList
        {
            get => (List<int>)GetValue(NumberListProperty);
            set => SetValue(NumberListProperty, value);
        }

        public static readonly DependencyProperty TotalSecondsProperty =
            DependencyProperty.Register(nameof(TotalSeconds), typeof(int), typeof(CountDownControl), new PropertyMetadata(0));

        /// <summary>
        /// 倒计时总时长
        /// </summary>
        public int TotalSeconds
        {
            get => (int)GetValue(TotalSecondsProperty);
            set => SetValue(TotalSecondsProperty, value);
        }

        /// <summary>
        /// 背景图片依赖项属性
        /// </summary>
        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register(nameof(BackgroundImage), typeof(ImageSource), typeof(CountDownControl), new PropertyMetadata(null));

        /// <summary>
        /// 背景图片
        /// </summary>
        public ImageSource BackgroundImage
        {
            get => (ImageSource)GetValue(BackgroundImageProperty);
            set => SetValue(BackgroundImageProperty, value);
        }

        #endregion

        #region EventHandlers

        private void CountDownControl_Loaded(object sender, RoutedEventArgs e)
        {
            BuildNumberList();

            mDispatcherTimer.Tick += DispatcherTimer_Tick;
            mDispatcherTimer.Start();
        }

        private void CountDownControl_Unloaded(object sender, RoutedEventArgs e)
        {
            mDispatcherTimer.Stop();
            mDispatcherTimer.Tick -= DispatcherTimer_Tick;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (ProcessCountDown())
            {
                BuildNumberList();
            }
            else
            {
                mDispatcherTimer.Stop();
            }
        }

        #endregion

        #region Methods  

        /// <summary>
        /// 减秒
        /// </summary>
        /// <returns></returns>
        public bool ProcessCountDown()
        {
            if (TotalSeconds == 0)
            {
                return false;
            }
            else
            {
                TotalSeconds--;
                return true;
            }
        }

        /// <summary>
        /// 构造分钟十位、个位和秒十位、个位数集合
        /// </summary>
        private void BuildNumberList()
        {
            mMinute = GetMinutes();
            mSecond = GetSeconds();

            NumberList = new List<int>()
            {
                mMinute / 10,
                mMinute % 10,
                mSecond / 10,
                mSecond % 10
            };
        }

        /// <summary>
        /// 获取小时显示值
        /// </summary>
        /// <returns></returns>
        public int GetHours()
        {
            return TotalSeconds / 3600;
        }

        /// <summary>
        /// 获取分钟显示值
        /// </summary>
        /// <returns></returns>
        public int GetMinutes()
        {
            return TotalSeconds % 3600 / 60;
        }

        /// <summary>
        /// 获取秒显示值
        /// </summary>
        /// <returns></returns>
        public int GetSeconds()
        {
            return TotalSeconds % 60;
        }

        public void Dispose()
        {
            if (mIsDisposed)
            {
                return;
            }

            Loaded -= CountDownControl_Loaded;
            Unloaded -= CountDownControl_Unloaded;
            mDispatcherTimer.Stop();
            mIsDisposed = true;

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
