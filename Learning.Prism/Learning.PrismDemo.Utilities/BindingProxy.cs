using System.Windows;

namespace Learning.PrismDemo.Utilities
{
	/// <summary>
	/// 绑定代理
	/// </summary>
	public class BindingProxy : Freezable
	{
		#region Overrides

		/// <summary>
		/// 初始化<see cref="System.Windows.Freezable"/> 类的一个新实例
		/// </summary>
		/// <returns></returns>
		protected override Freezable CreateInstanceCore()
		{
			return new BindingProxy();
		}

		#endregion

		/// <summary>
		/// 绑定的数据
		/// </summary>
		public object Data
		{
			get { return (object)GetValue(DataProperty); }
			set { SetValue(DataProperty, value); }
		}

		/// <summary>
		/// 绑定的数据的依赖项属性
		/// </summary>
		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
	}
}
