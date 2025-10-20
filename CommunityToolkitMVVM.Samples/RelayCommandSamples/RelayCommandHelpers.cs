using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RelayCommandSamples;

public static class RelayCommandHelpers
{
    private static readonly List<WeakReference<IRelayCommand>> _commands = new();

    public static void RegisterCommandsWithCommandManager(object container)
    {
        if (_commands.Count == 0)
            CommandManager.RequerySuggested += OnRequerySuggested;

        foreach (var p in container.GetType().GetProperties())
        {
            if (p.PropertyType.IsAssignableTo(typeof(IRelayCommand)))
            {
                var command = (IRelayCommand?)p.GetValue(container);
                if (command != null)
                    _commands.Add(new WeakReference<IRelayCommand>(command));
            }
        }
    }

    private static void OnRequerySuggested(object? sender, EventArgs args)
    {
        foreach (var command in _commands)
        {
            if (command.TryGetTarget(out var c))
                c.NotifyCanExecuteChanged();
        }
    }
}
