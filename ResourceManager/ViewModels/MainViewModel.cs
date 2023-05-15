using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.ViewModels;
using NetCore.Security;
using ResourceManager.Services.Abstractions;
using ResourceManager.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ResourceManager.ViewModels
{
    public class MainViewModel : PropertyChangedBase, IOnLoadedHandler, IPopupable
    {
        #region IOnLoadedHandler
        public Task OnLoadedAsync()
        {
            RefreshUI();
            return Task.FromResult(true);
        }
        #endregion
        public void RefreshUI()
        {
            if (AuthManager.User.Identity.IsAuthenticated)
                CurrentPage = MainPage;
            else
                CurrentPage = LoginPage;
        }
        #region IPopupable
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
        public void HidePopup()
        {
            IsPopupVisible = false;
            if (CurrentPopup != null)
            {
                CurrentPopup.CloseHandler -= PopupViewModel_CloseHandler;
                CurrentPopup = null;
            }
        }

        internal void GoHome()
        {
            CurrentPage = MainPage;
        }

        public bool IsPopupVisible
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public PopupViewModel? CurrentPopup
        {
            get => GetProperty<PopupViewModel?>();
            set => SetProperty(value);
        }
        #endregion

        public ViewModelBase? CurrentPage
        {
            get => GetProperty<ViewModelBase?>();
            set => SetProperty(value);
        }
        private ViewModelBase LoginPage => App.ServiceProvider.GetRequiredService<LoginPageViewModel>();
        private ViewModelBase MainPage => App.ServiceProvider.GetRequiredService<MainPageViewModel>();
    }
}
