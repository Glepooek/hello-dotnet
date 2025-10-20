using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

// https://blog.gitcode.com/c90b803703b2f8f6d62f91e4a13507f9.html
// https://github.com/CommunityToolkit/dotnet/discussions/240
// https://github.com/CommunityToolkit/MVVM-Samples/issues/41

//public event EventHandler? CanExecuteChanged
//{
//    add => CommandManager.RequerySuggested += value;
//    remove => CommandManager.RequerySuggested -= value;
//}

namespace RelayCommandSamples.ViewModels
{
    internal partial class MainCanExecutedViewModel : ObservableObject
    {
        #region Properties
        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        //private bool canExecute;

        private bool canExecute;
        #endregion

        #region Commands

        private RelayCommand mControlCommand;
        public RelayCommand ControlCommand => mControlCommand ??= new RelayCommand(OnControl);

        private void OnControl()
        {
            //CanExecute = !CanExecute;
            //SubmitCommand.NotifyCanExecuteChanged();

            canExecute = !canExecute;
        }

        private RelayCommand mSubmitCommand;
        public RelayCommand SubmitCommand => mSubmitCommand ??= new RelayCommand(OnSubmit, CanSubmit);

        private bool CanSubmit()
        {
            //return CanExecute;
            return canExecute;
        }

        private void OnSubmit()
        {
            Debug.WriteLine("Submit command executed.");
        }
        #endregion

        public MainCanExecutedViewModel()
        {
            RelayCommandHelpers.RegisterCommandsWithCommandManager(this);
        }
    }
}
