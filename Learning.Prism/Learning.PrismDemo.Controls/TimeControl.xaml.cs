using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Learning.PrismDemo.Controls
{
	/// <summary>
	/// 时间控件交互逻辑
	/// </summary>
	public partial class TimeControl : UserControl
	{
		#region Fields

		private DispatcherTimer m_Timer = null;

		#endregion

		#region DependencyProperty

		/// <summary>
		/// 当前时间
		/// </summary>
		public DateTime CurrentTime
		{
			get { return (DateTime)GetValue(CurrentTimeProperty); }
			set { SetValue(CurrentTimeProperty, value); }
		}

		/// <summary>
		/// 当前时间依赖项属性
		/// </summary>
		public static readonly DependencyProperty CurrentTimeProperty =
			DependencyProperty.Register(nameof(CurrentTime), typeof(DateTime), typeof(TimeControl), new PropertyMetadata(DateTime.Now));

		#endregion

		#region Construtor

		public TimeControl()
		{
			InitializeComponent();

			this.Loaded += TimeControl_Loaded;
			this.Unloaded += TimeControl_Unloaded;
		}

		#endregion

		#region EventHandlers

		private void TimeControl_Loaded(object sender, RoutedEventArgs e)
		{
			m_Timer = new DispatcherTimer(DispatcherPriority.Background)
			{
				Interval = TimeSpan.FromMilliseconds(1000)
			};

			m_Timer.Tick += OnTimeElapsed;
			m_Timer.Start();
		}

		private void TimeControl_Unloaded(object sender, RoutedEventArgs e)
		{
			m_Timer.Tick -= OnTimeElapsed;
			m_Timer.Stop();
		}

		private void OnTimeElapsed(object sender, EventArgs e)
		{
			CurrentTime = DateTime.Now;
		}

		#endregion
	}
}
