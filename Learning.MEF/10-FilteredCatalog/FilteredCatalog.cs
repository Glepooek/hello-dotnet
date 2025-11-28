using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Linq.Expressions;

namespace _10_FilteredCatalog
{
	public class FilteredCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		private readonly ComposablePartCatalog m_ComposablePartCatalog;
		private readonly INotifyComposablePartCatalogChanged m_NotifyComposablePartCatalogChanged;
		private readonly IQueryable<ComposablePartDefinition> m_Parts;

		/// <summary>
		/// 默认构造函数
		/// </summary>
		/// <param name="composablePartCatalog">包含所有导出部件的目录</param>
		/// <param name="expression">筛选条件表达式</param>
		public FilteredCatalog(ComposablePartCatalog composablePartCatalog,
							Expression<Func<ComposablePartDefinition, bool>> expression)
		{
			m_ComposablePartCatalog = composablePartCatalog;
			m_NotifyComposablePartCatalogChanged = composablePartCatalog as INotifyComposablePartCatalogChanged;
			m_Parts = composablePartCatalog.Parts.Where(expression);
		}

		/// <summary>
		/// 部件目录已经改变后触发的事件
		/// </summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed
		{
			add
			{
				if (m_NotifyComposablePartCatalogChanged != null)
					m_NotifyComposablePartCatalogChanged.Changed += value;
			}
			remove
			{
				if (m_NotifyComposablePartCatalogChanged != null)
					m_NotifyComposablePartCatalogChanged.Changed -= value;
			}
		}

		/// <summary>
		/// 部件目录正在发生改变时触发的事件
		/// </summary>
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing
		{
			add
			{
				if (m_NotifyComposablePartCatalogChanged != null)
					m_NotifyComposablePartCatalogChanged.Changing += value;
			}
			remove
			{
				if (m_NotifyComposablePartCatalogChanged != null)
					m_NotifyComposablePartCatalogChanged.Changing -= value;
			}
		}

		/// <summary>  
		/// 获取目录中包含的部件定义。
		/// 经过构造函数中的表达式过滤后，已经是传入目录Catalog对象中的一部分导出部件了。  
		/// </summary>  
		public override IQueryable<ComposablePartDefinition> Parts
		{
			get
			{
				return m_Parts;
			}
		}
	}
}
