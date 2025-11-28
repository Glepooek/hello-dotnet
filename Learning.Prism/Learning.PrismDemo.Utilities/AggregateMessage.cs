using Prism.Events;
using System;

namespace Learning.PrismDemo.Utilities
{
	/// <summary>
	/// 帮助订阅和发布聚合消息
	/// </summary>
	public abstract class AggregateMessage<T>
		where T : EventBase, new()
	{
		#region Fields

		/// <summary>
		/// 聚合消息全局管理对象
		/// </summary>
		protected static IEventAggregator m_EventAggregator = new EventAggregator();

		/// <summary>
		/// 聚合消息对象
		/// </summary>
		protected T m_Message;

		#endregion

		#region Constructors

		/// <summary>
		/// 构造函数
		/// </summary>
		public AggregateMessage()
		{
			m_Message = m_EventAggregator.GetEvent<T>();

			if (m_Message == null)
			{
				throw (new ArgumentNullException("无法从聚合器接口获取消息对象"));
			}
		}

		#endregion
	}

	/// <summary>
	/// 聚合消息的监听者
	/// </summary>
	public class AggregateMessageListener<T, TPara> : AggregateMessage<T>, IDisposable
		where T : PubSubEvent<TPara>, new()
	{
		#region Fields

		private Action<TPara> m_Callback;

		#endregion

		#region Constructors

		/// <summary>
		/// 聚合消息监听者构造器
		/// </summary>
		/// <param name="callback">监听回调</param>
		/// <param name="threadOption">监听者所在线程</param>
		public AggregateMessageListener(Action<TPara> callback, ThreadOption threadOption = ThreadOption.UIThread)
			: base()
		{
			m_Callback = callback;
			m_Message.Subscribe(callback, threadOption);
		}

		#endregion

		#region Public

		/// <summary>
		/// 退订消息
		/// </summary>
		public void Dispose()
		{
			if (m_Message != null)
			{
				m_Message.Unsubscribe(m_Callback);
			}
		}

		#endregion
	}

	/// <summary>
	/// 聚合消息的发布者
	/// </summary>
	public class AggregateMessageClient<T, TPara> : AggregateMessage<T>
		where T : PubSubEvent<TPara>, new()
	{
		#region Constructors

		/// <summary>
		/// 聚合消息的发布者构造器
		/// </summary>
		public AggregateMessageClient()
			: base()
		{
		}

		#endregion

		#region Public

		/// <summary>
		/// 发布消息
		/// </summary>
		/// <param name="data">发布的数据</param>
		public void Publish(TPara data)
		{
			if (m_Message != null)
			{
				m_Message.Publish(data);
			}
		}

		#endregion
	}
}
