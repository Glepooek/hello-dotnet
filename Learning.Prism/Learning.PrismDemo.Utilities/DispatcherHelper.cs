using System;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Threading;

namespace Learning.PrismDemo.Utilities
{
	public class DispatcherHelper
	{
		/// <summary>
		/// 在UI线程中执行操作
		/// </summary>
		/// <param name="action">操作</param>
		public static void InvokeOnUIThread(Action action)
		{
			Dispatcher dispatcher = (Application.Current != null && Application.Current.Dispatcher != null) ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher;
			if (dispatcher.CheckAccess())
			{
				action();
			}
			else
			{
				dispatcher.BeginInvoke(DispatcherPriority.Background, action);
			}
		}

		/// <summary>
		/// 在UI线程中执行操作
		/// </summary>
		/// <param name="action">操作</param>
		/// <param name="dispatcher">与当前线程关联的Dispatcher</param>
		public static void InvokeOnUIThread(Action action, Dispatcher dispatcher)
		{
			if (action == null || dispatcher == null)
			{
				return;
			}

			if (dispatcher.CheckAccess())
			{
				action();
			}
			else
			{
				dispatcher.BeginInvoke(DispatcherPriority.Background, action);
			}
		}

		/// <summary>
		/// 处理当前在消息队列中的所有UI消息
		/// <remarks>
		/// 按钮延迟执行：
		/// 	btnClick.IsEnabled = false;
		///	DispatcherHelper.DoEvents();
		///	System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
		///	Thread.Sleep(2000);
		///	btnClick.IsEnabled = true;
		/// </remarks>
		/// </summary>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void DoEvents()
		{
			// 创建新的嵌套消息泵
			DispatcherFrame frame = new DispatcherFrame();
			// 在被调用时向当前消息队列发送回调，此回调将结束嵌套消息循环。
			// 请注意，此回调的优先级应低于UI事件消息的优先级
			DispatcherOperation exitOperation =
				Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrames), frame);

			try
			{
				// 抽出嵌套的消息循环，嵌套的消息循环将立即处理留在消息队列中的消息
				Dispatcher.PushFrame(frame);
				// 如果“ exitFrame”回调未完成，请中止它
				if (exitOperation.Status != DispatcherOperationStatus.Completed)
				{
					exitOperation.Abort();
				}
			}
			catch (InvalidOperationException)
			{
			}
		}

		private static object ExitFrames(object frame)
		{
			if (frame is DispatcherFrame dispatcherFrame)
			{
				// 退出嵌套消息循环
				dispatcherFrame.Continue = false;
			}
			return null;
		}
	}
}
