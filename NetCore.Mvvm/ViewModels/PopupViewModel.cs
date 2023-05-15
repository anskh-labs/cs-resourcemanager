using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using System;
using System.Windows.Input;

namespace NetCore.Mvvm.ViewModels
{
    public abstract class PopupViewModel : ValidationPropertyChangedBase
    {
        public event EventHandler? CloseHandler;
        public PopupViewModel()
        {
            CloseCommand = new RelayCommand(OnClose);
        }
        protected void OnClose()
        {
            CloseHandler?.Invoke(this, EventArgs.Empty);
        }
        public PopupResult Result
        {
            get => GetProperty<PopupResult>();
            protected set => SetProperty(value);
        }
        public string Caption
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public ICommand CloseCommand { get; }
    }
}
