using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MemoryGameByJohnny.ViewModels
{
    /// <summary>
    /// Base class for MVVM ViewModels that implements <see cref="INotifyPropertyChanged"/>.
    /// Provides a lightweight <c>SetProperty</c> helper to update backing fields and raise
    /// change notifications in a single, safe call.
    ///
    /// Usage:
    /// - Back your public properties with a private field.
    /// - Call <see cref="SetProperty{T}(ref T, T, string?)"/> in the setter.
    ///
    /// Notes:
    /// - Raises <see cref="PropertyChanged"/> only when the value actually changes
    ///   (using <see cref="EqualityComparer{T}.Default"/> for comparison).
    /// - Designed for UI thread usage (typical WPF scenario).
    /// - Keep ViewModel logic here; do not put UI code in the ViewModel.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event fired when a property's value changes, notifying WPF bindings to refresh.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises <see cref="PropertyChanged"/> for the calling property.
        /// </summary>
        /// <param name="name">Property name (auto-supplied by the compiler).</param>
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Sets a backing field and raises <see cref="PropertyChanged"/> if the value actually changes.
        /// Use this from property setters to avoid boilerplate and accidental missed notifications.
        /// </summary>
        /// <typeparam name="T">Type of the property/field.</typeparam>
        /// <param name="field">Reference to the backing field.</param>
        /// <param name="value">New value to assign.</param>
        /// <param name="name">Property name (auto-supplied by the compiler).</param>
        /// <returns><c>true</c> if the value changed; otherwise <c>false</c>.</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(name);
            return true;
        }
    }
}
