using Prism.Mvvm;
using System.Windows.Controls;

namespace Learning.PrismDemo.Models
{
	public class SettingMenuItem : BindableBase
	{
		private string m_MenuItemName;
		/// <summary>
		/// 设置菜单项名称
		/// </summary>
		public string MenuItemName
		{
			get
			{
				return m_MenuItemName;
			}
			set
			{
				if (m_MenuItemName != value)
				{
					m_MenuItemName = value;
					RaisePropertyChanged();
				}
			}
		}

		private UserControl m_MenuItemContent;
		/// <summary>
		/// 设置菜单项选中后显示的内容
		/// </summary>
		public UserControl MenuItemContent
		{
			get
			{
				return m_MenuItemContent;
			}
			set
			{
				if (m_MenuItemContent != value)
				{
					m_MenuItemContent = value;
					RaisePropertyChanged();
				}
			}
		}
	}
}
