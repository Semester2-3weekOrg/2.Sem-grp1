using System.Windows.Input;

namespace TheMovies.Commands
{
    /// <summary>
    /// Represents a command that can be executed in response to user interaction or other events.
    /// </summary>
    /// <remarks>This class provides an implementation of the <see cref="ICommand"/> interface, allowing you
    /// to define commands with custom execution logic and optional conditions for whether the command can execute. Use
    /// this class to bind commands to UI elements in MVVM applications.</remarks>
    //public class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
    //{
    //    private readonly Action _execute = execute;
    //    private readonly Func<bool>? _canExecute = canExecute;

    //    public event EventHandler? CanExecuteChanged
    //    {
    //        add => CommandManager.RequerySuggested += value;
    //        remove => CommandManager.RequerySuggested -= value;
    //    }

    //    public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();
    //    public void Execute(object? parameter) => _execute();
    //}

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
