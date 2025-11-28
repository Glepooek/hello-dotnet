using System;
using System.Windows;
using System.Windows.Controls;

namespace Learning.PrismDemo.Controls
{
	/// <summary>
	/// 时间选择控件
	/// </summary>
	[TemplatePart(Name = TimePickerControl.ElementHourTextBox, Type = typeof(TextBox))]
	[TemplatePart(Name = TimePickerControl.ElementMinuteTextBox, Type = typeof(TextBox))]
	[TemplatePart(Name = TimePickerControl.ElementSecondTextBox, Type = typeof(TextBox))]
	[TemplatePart(Name = TimePickerControl.ElementIncrementButton, Type = typeof(Button))]
	[TemplatePart(Name = TimePickerControl.ElementDecrementButton, Type = typeof(Button))]
	public class TimePickerControl : Control
	{
		#region Fields

		private const string ElementHourTextBox = "PART_HourTextBox";
		private const string ElementMinuteTextBox = "PART_MinuteTextBox";
		private const string ElementSecondTextBox = "PART_SecondTextBox";
		private const string ElementIncrementButton = "PART_IncrementButton";
		private const string ElementDecrementButton = "PART_DecrementButton";

		/// <summary>
		/// 最小初始值
		/// </summary>
		private static readonly TimeSpan MinValidTime = new TimeSpan(0, 0, 0);
		/// <summary>
		/// 最大初始值
		/// </summary>
		private static readonly TimeSpan MaxValidTime = new TimeSpan(23, 59, 59);

		private TextBox m_HourTextBox = null;
		private TextBox m_MinuteTextBox = null;
		private TextBox m_SecondTextBox = null;
		private TextBox m_SelectedTextBox = null;
		private Button m_IncrementButton = null;
		private Button m_DecrementButton = null;

		#endregion

		#region Dependency Properties

		/// <summary>
		/// 选中时间
		/// </summary>
		public TimeSpan SelectedTime
		{
			get { return (TimeSpan)GetValue(SelectedTimeProperty); }
			set { SetValue(SelectedTimeProperty, value); }
		}

		/// <summary>
		/// 选中时间依赖项属性
		/// </summary>
		public static readonly DependencyProperty SelectedTimeProperty =
			DependencyProperty.Register(nameof(SelectedTime), typeof(TimeSpan), typeof(TimePickerControl),
				new FrameworkPropertyMetadata(MinValidTime, new PropertyChangedCallback(OnSelectedTimeChanged), new CoerceValueCallback(CoerceSelectedTime)));


		/// <summary>
		/// 最小时间
		/// </summary>
		public TimeSpan MinTime
		{
			get { return (TimeSpan)GetValue(MinTimeProperty); }
			set { SetValue(MinTimeProperty, value); }
		}

		/// <summary>
		/// 最小时间依赖项属性
		/// </summary>
		public static readonly DependencyProperty MinTimeProperty =
			DependencyProperty.Register(nameof(MinTime), typeof(TimeSpan), typeof(TimePickerControl),
				new FrameworkPropertyMetadata(MinValidTime, new PropertyChangedCallback(OnMinTimeChanged)),
				new ValidateValueCallback(IsValidTime));


		/// <summary>
		/// 最大时间
		/// </summary>
		public TimeSpan MaxTime
		{
			get { return (TimeSpan)GetValue(MaxTimeProperty); }
			set { SetValue(MaxTimeProperty, value); }
		}

		/// <summary>
		/// 最大时间依赖项属性
		/// </summary>
		public static readonly DependencyProperty MaxTimeProperty =
			DependencyProperty.Register(nameof(MaxTime), typeof(TimeSpan), typeof(TimePickerControl),
				new FrameworkPropertyMetadata(MaxValidTime, new PropertyChangedCallback(OnMaxTimeChanged), new CoerceValueCallback(CoerceMaxTime)),
				new ValidateValueCallback(IsValidTime));

		#endregion

		#region Event

		public event RoutedPropertyChangedEventHandler<TimeSpan> SelectedTimeChanged
		{
			add { base.AddHandler(SelectedTimeChangedEvent, value); }
			remove { base.RemoveHandler(SelectedTimeChangedEvent, value); }
		}

		public static readonly RoutedEvent SelectedTimeChangedEvent =
			EventManager.RegisterRoutedEvent(nameof(SelectedTimeChanged), RoutingStrategy.Bubble,
								typeof(RoutedPropertyChangedEventHandler<TimeSpan>), typeof(TimePickerControl));

		#endregion

		#region Constructor

		static TimePickerControl()
		{
			// 指定默认样式
			// 从themes/generic.xaml文件中查找，不指定key
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePickerControl), new FrameworkPropertyMetadata(typeof(TimePickerControl)));
		}

		public TimePickerControl()
		{
			SelectedTime = DateTime.Now.TimeOfDay;
		}

		#endregion

		#region Public Methods

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			m_HourTextBox = GetTemplateChild(ElementHourTextBox) as TextBox;
			if (m_HourTextBox != null)
			{
				m_HourTextBox.IsReadOnly = true;
				m_HourTextBox.GotFocus += OnSelectedTextBox;
			}

			m_MinuteTextBox = GetTemplateChild(ElementMinuteTextBox) as TextBox;
			if (m_HourTextBox != null)
			{
				m_MinuteTextBox.IsReadOnly = true;
				m_MinuteTextBox.GotFocus += OnSelectedTextBox;
			}

			m_SecondTextBox = GetTemplateChild(ElementSecondTextBox) as TextBox;
			if (m_SecondTextBox != null)
			{
				m_SecondTextBox.IsReadOnly = true;
				m_SecondTextBox.GotFocus += OnSelectedTextBox;
			}

			m_IncrementButton = GetTemplateChild(ElementIncrementButton) as Button;
			if (m_IncrementButton != null)
			{
				m_IncrementButton.Click += OnIncrementTime;
			}

			m_DecrementButton = GetTemplateChild(ElementDecrementButton) as Button;
			if (m_DecrementButton != null)
			{
				m_DecrementButton.Click += OnDecrementTime;
			}
		}

		#endregion

		#region Private Methods

		private void OnSelectedTextBox(object sender, RoutedEventArgs e)
		{
			m_SelectedTextBox = sender as TextBox;
		}

		private void OnDecrementTime(object sender, RoutedEventArgs e)
		{
			IncrementDecrementTime(-1);
		}

		private void OnIncrementTime(object sender, RoutedEventArgs e)
		{
			IncrementDecrementTime(1);
		}

		private void IncrementDecrementTime(int step)
		{
			if (m_SelectedTextBox == null)
			{
				m_SelectedTextBox = m_HourTextBox;
			}

			TimeSpan time;
			if (m_SelectedTextBox.Equals(m_HourTextBox))
			{
				time = SelectedTime.Add(new TimeSpan(step, 0, 0));
			}
			else if (m_SelectedTextBox.Equals(m_MinuteTextBox))
			{
				time = SelectedTime.Add(new TimeSpan(0, step, 0));
			}
			else
			{
				time = SelectedTime.Add(new TimeSpan(0, 0, step));
			}

			SelectedTime = time;
		}

		#endregion

		#region EventHandlers

		private static void OnSelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TimePickerControl timePicker = d as TimePickerControl;
			timePicker.OnSelectedTimeChanged((TimeSpan)e.OldValue, (TimeSpan)e.NewValue);
		}

		protected virtual void OnSelectedTimeChanged(TimeSpan oldSelectedTime, TimeSpan newSelectedTime)
		{
			RoutedPropertyChangedEventArgs<TimeSpan> e = new RoutedPropertyChangedEventArgs<TimeSpan>(oldSelectedTime, newSelectedTime)
			{
				RoutedEvent = SelectedTimeChangedEvent
			};
			base.RaiseEvent(e);
		}

		private static object CoerceSelectedTime(DependencyObject d, object value)
		{
			TimePickerControl timePicker = d as TimePickerControl;
			TimeSpan minTime = timePicker.MinTime;
			TimeSpan maxTime = timePicker.MaxTime;

			if ((TimeSpan)value < minTime)
			{
				return minTime;
			}

			if ((TimeSpan)value > maxTime)
			{
				return maxTime;
			}

			return value;
		}

		private static void OnMinTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TimePickerControl timePicker = d as TimePickerControl;
			timePicker.CoerceValue(MaxTimeProperty);
			timePicker.CoerceValue(SelectedTimeProperty);
			timePicker.OnMinTimeChanged((TimeSpan)e.OldValue, (TimeSpan)e.NewValue);
		}

		protected virtual void OnMinTimeChanged(TimeSpan oldMinTime, TimeSpan newMinTime)
		{

		}

		private static void OnMaxTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TimePickerControl timePicker = d as TimePickerControl;
			timePicker.CoerceValue(SelectedTimeProperty);
			timePicker.OnMaxTimeChanged((TimeSpan)e.OldValue, (TimeSpan)e.NewValue);
		}

		protected virtual void OnMaxTimeChanged(TimeSpan oldMinTime, TimeSpan newMinTime)
		{

		}

		private static object CoerceMaxTime(DependencyObject d, object value)
		{
			TimePickerControl timePicker = d as TimePickerControl;
			TimeSpan minTime = timePicker.MinTime;

			if ((TimeSpan)value < minTime)
			{
				return minTime;
			}

			return value;
		}

		/// <summary>
		/// 验证设定的最大时间和最小时间是否有效
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static bool IsValidTime(object value)
		{
			TimeSpan time = (TimeSpan)value;
			return time >= MinValidTime && time <= MaxValidTime;
		}

		#endregion
	}
}
