using System.Windows.Input;

namespace EmployeeManager
{
    public static class StateMachineExtensions
    {
        public static ICommand CreateCommand<TState, TTrigger>(this Stateless.StateMachine<TState, TTrigger> stateMachine, TTrigger trigger)
        {
            return new RelayCommand
              (
                () => stateMachine.Fire(trigger),
                () => stateMachine.CanFire(trigger)
              );
        }
    }
}
