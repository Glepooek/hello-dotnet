using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

/**
 * SubstateOf(TState state)——表示一个状态有一个超状态并且将继承它的所有配置。
 * Permit(TTrigger trigger, TState targetState)——允许状态通过触发器转换到目标状态。
 * Ignore(TTrigger trigger)——如果触发器被触发，则导致状态忽略触发器。
 * OnEntry(Action entryAction)——在进入状态时执行一个动作。
 * OnExit(Action exitAction)——在状态退出时导致一个动作执行。
 * void Fire(TTrigger)——使用之前的配置转换状态机。
 * bool CanFire(Trigger trigger)——如果当前状态允许触发触发器，则返回真。
 **/

namespace EmployeeManager
{
    public enum States
    {
        Start, Searching, SearchComplete, Selected, NoSelection, Editing
    }

    public enum Triggers
    {
        Search, SearchFailed, SearchSucceeded, Select, DeSelect, Edit, EndEdit
    }

    public class StateMachine : Stateless.StateMachine<States, Triggers>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public StateMachine(Action searchAction) : base(States.Start)
        {
            Configure(States.Start)
              .Permit(Triggers.Search, States.Searching);

            Configure(States.Searching)
              .OnEntry(searchAction)
              .Permit(Triggers.SearchSucceeded, States.SearchComplete)
              .Permit(Triggers.SearchFailed, States.Start)
              .Ignore(Triggers.Select)
              .Ignore(Triggers.DeSelect);

            Configure(States.SearchComplete)
              .SubstateOf(States.Start)
              .Permit(Triggers.Select, States.Selected)
              .Permit(Triggers.DeSelect, States.NoSelection);

            Configure(States.Selected)
              .SubstateOf(States.SearchComplete)
              .Permit(Triggers.DeSelect, States.NoSelection)
              .Permit(Triggers.Edit, States.Editing)
              .Ignore(Triggers.Select);

            Configure(States.NoSelection)
              .SubstateOf(States.SearchComplete)
              .Permit(Triggers.Select, States.Selected)
              .Ignore(Triggers.DeSelect);

            Configure(States.Editing)
              .Permit(Triggers.EndEdit, States.Selected);

            OnTransitioned
              (
                (t) =>
                {
                    OnPropertyChanged(nameof(State));
                    CommandManager.InvalidateRequerySuggested();
                }
              );

            //used to debug commands and UI components
            OnTransitioned
              (
                (t) => Debug.WriteLine
                  (
                    "State Machine transitioned from {0} -> {1} [{2}]",
                    t.Source, t.Destination, t.Trigger
                  )
              );
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
