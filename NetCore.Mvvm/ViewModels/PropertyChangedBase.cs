using NetCore.Mvvm.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NetCore.Mvvm.ViewModels
{
    /// <summary>
    /// Base class implementing <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Storage for fields
        /// </summary>
        protected Dictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Notifies clients that all properties may have changed.
        /// </summary>
        /// <remarks>
        /// This method raises the <see cref="PropertyChanged"/> event with <see cref="string.Empty"/> as the property name.
        /// </remarks>
        protected void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
            OnPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="property">The changed property.</param>
        /// <typeparam name="TProperty">The type of the changed property.</typeparam>
        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            OnPropertyChanged(ExpressionHelper.GetMemberName(property));
        }

        /// <summary>
        /// Set field value and Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual bool SetProperty<TProperty>(TProperty value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<TProperty>.Default.Equals(GetProperty<TProperty>(propertyName), value)) return false;
            if (!string.IsNullOrEmpty(propertyName))
            {
                _properties[propertyName] = value;
                OnPropertyChanged(propertyName);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Set field value and Raises the <see cref="PropertyChanged"/> event. 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual bool SetProperty<TProperty>(TProperty value, Expression<Func<TProperty>> property)
        {
            return SetProperty<TProperty>(value, ExpressionHelper.GetMemberName(property));
        }

        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual TProperty GetProperty<TProperty>([CallerMemberName] string? propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                object? value = null;
                if (_properties.TryGetValue(propertyName, out value))
                    return value == null ? default(TProperty) : (TProperty)value;
            }
            return default(TProperty);
        }

        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual TProperty? GetProperty<TProperty>(Expression<Func<TProperty>> property)
        {
            return GetProperty<TProperty>(ExpressionHelper.GetMemberName(property));
        }

    }
}
