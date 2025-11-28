using System;

namespace SocketClient
{
	public sealed class SingletonProvider<T> where T : class
	{
		/// <summary>
		/// 私有构造函数，不对外公开
		/// </summary>
		private SingletonProvider() { }

		/// <summary>
		/// 互斥锁，保证线程安全
		/// </summary>
		private static readonly object syncObject = new object();

		private static T m_Instance;
		/// <summary>
		/// 实例化对象
		/// </summary>
		public static T Instance
		{
			get
			{
				if (m_Instance == null)
				{
					lock (syncObject)
					{
						if (m_Instance == null)
						{
							m_Instance = (T)Activator.CreateInstance(typeof(T), true);
						}
					}
				}
				return m_Instance;
			}
		}
	}
}
