using System;

namespace Learning.PrismDemo.Utilities
{
	/// <summary>
	/// 单例提供类
	/// </summary>
	/// <typeparam name="T">需要单例的类类型</typeparam>
	public sealed class SingletonProvider<T> where T : class
	{
		#region Fields

		/// <summary>
		/// 互斥锁，保证线程安全
		/// </summary>
		private static readonly object syncObj = new object();

		#endregion

		#region Property

		private static T m_Instance = null;
		/// <summary>
		/// T类型实例
		/// </summary>
		public static T Instance
		{
			get
			{
				if (m_Instance == null)
				{
					lock (syncObj)
					{
						if (m_Instance == null)
						{
							// 用非公开构造函数创建T类型实例
							m_Instance = (T)Activator.CreateInstance(typeof(T), true);
						}
					}
				}

				return m_Instance;
			}
		}

		#endregion
	}
}
