using Prism.Services.Dialogs;
using System;

namespace Learning.PrismDemo.Controls.DialogServices
{
	public static class DialogServiceExtensions
	{
		/// <summary>
		/// 弹窗提示
		/// </summary>
		/// <remarks>
		/// 有确认、取消按钮
		/// </remarks>
		/// <param name="dialogService"></param>
		/// <param name="message">提示消息</param>
		/// <param name="callBack">回调函数</param>
		/// <param name="ismodal">是否模态</param>
		public static void ShowConfirm(this IDialogService dialogService, string message, Action<IDialogResult> callBack, bool ismodal = true)
		{
			if (ismodal)
			{
				dialogService.ShowDialog(DialogConstant.CONFIRM_DIALOG, new DialogParameters($"message={message}"), callBack);
			}
			else
			{
				dialogService.Show(DialogConstant.CONFIRM_DIALOG, new DialogParameters($"message={message}"), callBack);
			}
		}

		/// <summary>
		/// 弹窗提示
		/// </summary>
		/// <remarks>
		/// 无取消按钮，只有确认按钮。
		/// 无回调。
		/// </remarks>
		/// <param name="dialogService"></param>
		/// <param name="message">提示消息</param>
		/// <param name="ismodal">是否模态</param>
		public static void ShowAlert(this IDialogService dialogService, string message, bool ismodal = true)
		{
			DialogServiceExtensions.ShowAlert(dialogService, message, null, ismodal);
		}

		/// <summary>
		/// 弹窗提示
		/// </summary>
		/// <remarks>
		/// 无取消按钮，只有确认按钮。
		/// 有回调。
		/// </remarks>
		/// <param name="dialogService"></param>
		/// <param name="message">提示消息</param>
		/// <param name="callBack">回调函数</param>
		/// <param name="ismodal">是否模态</param>
		public static void ShowAlert(this IDialogService dialogService, string message, Action<IDialogResult> callBack, bool ismodal = true)
		{
			if (ismodal)
			{
				dialogService.ShowDialog(DialogConstant.ALERT_DIALOG, new DialogParameters($"message={message}"), callBack);
			}
			else
			{
				dialogService.Show(DialogConstant.ALERT_DIALOG, new DialogParameters($"message={message}"), callBack);
			}
		}

		/// <summary>
		/// 弹窗提示
		/// </summary>
		/// <remarks>
		/// 无确认取消按钮，在达到设定时间后自动关闭弹窗。
		/// 无回调。
		/// </remarks>
		/// <param name="dialogService"></param>
		/// <param name="message">提示消息</param>
		/// <param name="interval">timer执行时间间隔(毫秒)</param>
		/// <param name="ismodal">是否模态</param>
		public static void ShowNotification(this IDialogService dialogService, string message, double interval = 2000, bool ismodal = true)
		{
			DialogServiceExtensions.ShowNotification(dialogService, message, null, interval, ismodal);
		}

		/// <summary>
		/// 弹窗提示
		/// </summary>
		/// <remarks>
		/// 无确认取消按钮，在达到设定时间后自动关闭弹窗
		/// 有回调。
		/// </remarks>
		/// <param name="dialogService"></param>
		/// <param name="message">提示消息</param>
		/// <param name="callBack">回调函数</param>
		/// <param name="interval">timer执行时间间隔(毫秒)</param>
		/// <param name="ismodal">是否模态</param>
		public static void ShowNotification(this IDialogService dialogService, string message, Action<IDialogResult> callBack, double interval = 2000, bool ismodal = true)
		{
			if (ismodal)
			{
				dialogService.ShowDialog(DialogConstant.NOTIFICATION_DIALOG, new DialogParameters($"message={message}&interval={interval}"), callBack);
			}
			else
			{
				dialogService.Show(DialogConstant.NOTIFICATION_DIALOG, new DialogParameters($"message={message}&interval={interval}"), callBack);
			}
		}
	}
}
