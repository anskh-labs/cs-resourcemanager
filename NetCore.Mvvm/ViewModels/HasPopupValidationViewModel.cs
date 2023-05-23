using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NetCore.Mvvm.ViewModels
{

    public abstract class HasPopupValidationViewModel : ValidationPropertyChangedBase, IPopupable
    {
        public PopupViewModel? CurrentPopup 
        { 
            get => GetProperty<PopupViewModel>();
            set => SetProperty(value);
        }
        public bool IsPopupVisible 
        { 
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public void HidePopup()
        {
            IsPopupVisible = false;
            if (CurrentPopup != null)
            {
                CurrentPopup.CloseHandler -= PopupViewModel_CloseHandler;
                CurrentPopup = null;
            }
        }

        public PopupViewModel? ShowPoup(PopupViewModel popupViewModel)
        {
            popupViewModel.CloseHandler += PopupViewModel_CloseHandler;
            CurrentPopup = popupViewModel;
            IsPopupVisible = true;

            while (IsPopupVisible)
            {
                if (Dispatcher.CurrentDispatcher.HasShutdownStarted || Dispatcher.CurrentDispatcher.HasShutdownFinished) break;
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return popupViewModel;
        }
        private void PopupViewModel_CloseHandler(object? sender, EventArgs e)
        {
            HidePopup();
        }
    }
}
