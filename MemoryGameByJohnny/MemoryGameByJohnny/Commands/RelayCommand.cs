using System;
using System.Windows.Input;

namespace MemoryGameByJohnny.Commands
{
    /// <summary>
    /// A lightweight <see cref="ICommand"/> implementation for WPF/MVVM.
    /// Wraps an <c>Action&lt;object?&gt;</c> to execute and an optional
    /// <c>Func&lt;object?, bool&gt;</c> to determine whether the command can execute.
    ///
    /// Usage:
    /// - Bind to Buttons/MenuItems in XAML.
    /// - Provide business logic via delegates from your ViewModel.
    ///
    /// Notes:
    /// - If no <c>canExecute</c> is provided, the command is always enabled.
    /// - Hooks into <see cref="CommandManager.RequerySuggested"/> so WPF can
    ///   automatically re-evaluate CanExecute (e.g., on focus changes).
    /// - Call <see cref="RaiseCanExecuteChanged"/> when your command’s ability
    ///   to execute changes (e.g., state flips from "Evaluating" to "Idle").
    /// - Intended for use on the UI thread (not thread-safe).
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// Creates a new <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">The action to run when the command executes.</param>
        /// <param name="canExecute">
        /// Optional predicate that returns whether the command can execute
        /// for the given parameter. If <c>null</c>, the command is always enabled.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is null.</exception>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether the command can execute with the provided parameter.
        /// Returns <c>true</c> if <c>canExecute</c> is not supplied.
        /// </summary>
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Executes the command's action with the provided parameter.
        /// </summary>
        public void Execute(object? parameter) => _execute(parameter);

        /// <summary>
        /// WPF listens to this event to know when it should re-query <see cref="CanExecute(object)"/>.
        /// Internally we hook/unhook to <see cref="CommandManager.RequerySuggested"/> so the
        /// UI updates automatically during typical WPF input cycles.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Triggers a re-evaluation of <see cref="CanExecute(object)"/> across the UI.
        /// Call this after relevant state changes in your ViewModel.
        /// </summary>
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}
