using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Learning.PrismDemo.Controls.DialogServices
{
	public class AlertDialogViewModel : BindableBase, IDialogAware
	{
		#region Properties

		private string _message;
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

		#region Commands

		private DelegateCommand<string> _closeDialogCommand;
		public DelegateCommand<string> CloseDialogCommand => _closeDialogCommand ??= new DelegateCommand<string>(CloseDialog);

		private void CloseDialog(string parameter)
		{
			ButtonResult result;
			if (parameter?.ToLower() == "true")
			{
				result = ButtonResult.OK;
			}
			else
			{
				result = ButtonResult.None;
			}

			RaiseRequestClose(new DialogResult(result));
		}

		private void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}

		#endregion

		#region IDialogAwareMember

		public event Action<IDialogResult> RequestClose;

		public virtual bool CanCloseDialog()
		{
			return true;
		}

		public virtual void OnDialogClosed()
		{

		}

		public virtual void OnDialogOpened(IDialogParameters parameters)
		{
			Message = parameters?.GetValue<string>("message");
		}

		#endregion
	}
}
