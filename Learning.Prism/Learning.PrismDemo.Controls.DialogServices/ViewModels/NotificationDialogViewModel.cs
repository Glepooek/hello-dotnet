using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Threading;

namespace Learning.PrismDemo.Controls.DialogServices
{
	public class NotificationDialogViewModel : BindableBase, IDialogAware
	{
		#region Fields

		private readonly DispatcherTimer dispatcherTimer = null;

		#endregion

		#region Properties

		private string _message = string.Empty;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		private string _title = "提示";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		#endregion

		#region Constructor

		public NotificationDialogViewModel()
		{
			dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += OnTimerElapsed;
		}

		private void OnTimerElapsed(object sender, EventArgs e)
		{
			RaiseRequestClose(new DialogResult(ButtonResult.OK));
		}

		private void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}

		#endregion

		#region IDialogAwareMembers

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			dispatcherTimer.Tick -= OnTimerElapsed;
			dispatcherTimer?.Stop();
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
			double interval = parameters.GetValue<double>("interval");
			Message = parameters?.GetValue<string>("message");

			dispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);
			dispatcherTimer?.Start();
		}

		#endregion
	}
}
