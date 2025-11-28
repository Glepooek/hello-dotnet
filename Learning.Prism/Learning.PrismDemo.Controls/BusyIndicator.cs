using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Learning.PrismDemo.Controls
{
	/// <summary>
	/// Loading控件
	/// </summary>
	/// <remarks>
	/// 当程序处理耗时操作时，用于指示繁忙状态
	/// </remarks>
	[StyleTypedProperty(Property = "OverlayStyle", StyleTargetType = typeof(Rectangle))]
	[TemplateVisualState(Name = "Hidden", GroupName = "VisibilityStates")]
	[TemplateVisualState(Name = "Visible", GroupName = "VisibilityStates")]
	[TemplateVisualState(Name = "Idle", GroupName = "BusyStates")]
	[TemplateVisualState(Name = "Busy", GroupName = "BusyStates")]
	public class BusyIndicator : Control
	{
		#region DependencyProperties

		/// <summary>
		/// 是否处于繁忙状态
		/// </summary>
		public bool IsBusy
		{
			get { return (bool)GetValue(IsBusyProperty); }
			set { SetValue(IsBusyProperty, value); }
		}

		/// <summary>
		/// 是否处于繁忙状态依赖项属性
		/// </summary>
		public static readonly DependencyProperty IsBusyProperty =
			DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(BusyIndicator),
				new PropertyMetadata(false, new PropertyChangedCallback(OnIsBusyChanged)));

		/// <summary>
		/// 繁忙状态显示内容
		/// </summary>
		public object BusyContent
		{
			get { return GetValue(BusyContentProperty); }
			set { SetValue(BusyContentProperty, value); }
		}

		/// <summary>
		/// 繁忙状态显示内容依赖项属性
		/// </summary>
		public static readonly DependencyProperty BusyContentProperty =
			DependencyProperty.Register(nameof(BusyContent), typeof(object), typeof(BusyIndicator), new PropertyMetadata(null));

		/// <summary>
		/// 繁忙状态内容模板
		/// </summary>
		public DataTemplate BusyContentTemplate
		{
			get { return (DataTemplate)GetValue(BusyContentTemplateProperty); }
			set { SetValue(BusyContentTemplateProperty, value); }
		}

		/// <summary>
		/// 繁忙状态内容模板依赖项属性
		/// </summary>
		public static readonly DependencyProperty BusyContentTemplateProperty =
			DependencyProperty.Register(nameof(BusyContentTemplate), typeof(DataTemplate), typeof(BusyIndicator), new PropertyMetadata(null));

		/// <summary>
		/// Overlay样式
		/// </summary>
		public Style OverlayStyle
		{
			get { return (Style)GetValue(OverlayStyleProperty); }
			set { SetValue(OverlayStyleProperty, value); }
		}

		/// <summary>
		/// Overlay样式依赖项属性
		/// </summary>
		public static readonly DependencyProperty OverlayStyleProperty =
			DependencyProperty.Register(nameof(OverlayStyle), typeof(Style), typeof(BusyIndicator), new PropertyMetadata(null));

		/// <summary>
		/// 关闭Loading命令
		/// </summary>
		public ICommand CloseCommand
		{
			get { return (ICommand)GetValue(CloseCommandProperty); }
			set { SetValue(CloseCommandProperty, value); }
		}

		/// <summary>
		/// 关闭Loading命令依赖项属性
		/// </summary>
		public static readonly DependencyProperty CloseCommandProperty =
			DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(BusyIndicator), new PropertyMetadata(null));

		#endregion

		#region Constructors

		/// <summary>
		/// 构造函数
		/// </summary>
		public BusyIndicator()
		{
			DefaultStyleKey = typeof(BusyIndicator);
		}

		#endregion

		#region Overrides

		/// <summary>
		/// 该方法在ApplyTemplate()执行后触发
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			ChangeVisualState(false);
		}

		#endregion

		#region Methods

		private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BusyIndicator)d).OnIsBusyChanged(e);
		}

		private void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
		{
			ChangeVisualState(true);
		}

		/// <summary>
		/// 改变ControlTemplate中的VisualState
		/// </summary>
		/// <param name="useTransitions">是否调用动画</param>
		private void ChangeVisualState(bool useTransitions)
		{
			VisualStateManager.GoToState(this, IsBusy ? "Busy" : "Idle", useTransitions);
			VisualStateManager.GoToState(this, IsBusy ? "Visible" : "Hidden", useTransitions);
		}

		#endregion
	}
}