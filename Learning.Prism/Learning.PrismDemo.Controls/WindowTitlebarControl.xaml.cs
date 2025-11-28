using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace Learning.PrismDemo.Controls
{
	/// <summary>
	/// 窗口Titlebar控件交互逻辑
	/// </summary>
	public partial class WindowTitlebarControl : UserControl
	{
		#region DependencyProperties

		/// <summary>
		/// 设置按钮显示状态
		/// </summary>
		public Visibility SettingButtonVisiblity
		{
			get { return (Visibility)GetValue(SettingButtonVisiblityProperty); }
			set { SetValue(SettingButtonVisiblityProperty, value); }
		}

		/// <summary>
		/// 设置按钮显示状态依赖项属性
		/// </summary>
		public static readonly DependencyProperty SettingButtonVisiblityProperty =
			DependencyProperty.Register(nameof(SettingButtonVisiblity), typeof(Visibility), typeof(WindowTitlebarControl), new PropertyMetadata(Visibility.Collapsed));


		/// <summary>
		/// 是否自动触发最大化按钮
		/// </summary>
		public bool IsAutomationMaxButton
		{
			get { return (bool)GetValue(IsAutomationMaxButtonProperty); }
			set { SetValue(IsAutomationMaxButtonProperty, value); }
		}

		/// <summary>
		/// 是否自动触发最大化按钮依赖项属性
		/// </summary>
		public static readonly DependencyProperty IsAutomationMaxButtonProperty =
			DependencyProperty.Register(nameof(IsAutomationMaxButton), typeof(bool), typeof(WindowTitlebarControl), new PropertyMetadata(false, (o, e) =>
			{
				if (e.NewValue is bool isAutomation && isAutomation)
				{
					ToggleButtonAutomationPeer peer = new ToggleButtonAutomationPeer((o as WindowTitlebarControl).maxbtn);
					IToggleProvider toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
					toggleProvider.Toggle();
				}
			}));

		#endregion

		#region Constructor

		public WindowTitlebarControl()
		{
			InitializeComponent();
		}

		#endregion
	}
}
