using System.Windows;
using System.Windows.Controls;

namespace Learning.PrismDemo.Main.Views
{
	/// <summary>
	/// NavigationView交互逻辑
	/// </summary>
	public partial class NavigationView : UserControl
	{
		#region DependencyProperties

		/// <summary>
		/// 菜单项实际宽度
		/// </summary>
		public double MenuItemWidth
		{
			get { return (double)GetValue(MenuItemWidthProperty); }
			set { SetValue(MenuItemWidthProperty, value); }
		}
		/// <summary>
		/// 菜单项实际宽度依赖项属性
		/// </summary>
		public static readonly DependencyProperty MenuItemWidthProperty =
			DependencyProperty.Register(nameof(MenuItemWidth), typeof(double), typeof(NavigationView), new PropertyMetadata(400.0));

		/// <summary>
		/// 菜单项实际高度
		/// </summary>
		public double MenuItemHeight
		{
			get { return (double)GetValue(MenuItemHeightProperty); }
			set { SetValue(MenuItemHeightProperty, value); }
		}
		/// <summary>
		/// 菜单项实际高度依赖项属性
		/// </summary>
		public static readonly DependencyProperty MenuItemHeightProperty =
			DependencyProperty.Register(nameof(MenuItemHeight), typeof(double), typeof(NavigationView), new PropertyMetadata(230.0));

		/// <summary>
		/// 菜单项间距
		/// </summary>
		public Thickness MenuItemMargin
		{
			get { return (Thickness)GetValue(MenuItemMarginProperty); }
			set { SetValue(MenuItemMarginProperty, value); }
		}
		/// <summary>
		/// 菜单项间距依赖项属性
		/// </summary>
		public static readonly DependencyProperty MenuItemMarginProperty =
			DependencyProperty.Register(nameof(MenuItemMargin), typeof(Thickness), typeof(NavigationView), new PropertyMetadata(new Thickness(5)));

		#endregion

		#region Constructor

		public NavigationView()
		{
			InitializeComponent();
		}

		#endregion

		#region EventHandlers

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			ChangeMenuSize();
		}

		private void ChangeMenuSize()
		{
			if (sv_MenuPanel.ActualWidth > 0.0)
			{
				//30：左右margin的和，30：滚动条预留位置,3：横向显示3列
				MenuItemWidth = (sv_MenuPanel.ActualWidth - 30 - 30) / 3;
			}
			if (sv_MenuPanel.ActualHeight > 0.0)
			{
				//30：上下margin的和,3：纵向显示3行
				MenuItemHeight = (sv_MenuPanel.ActualHeight - 30) / 3;
			}
		}

		#endregion
	}
}
