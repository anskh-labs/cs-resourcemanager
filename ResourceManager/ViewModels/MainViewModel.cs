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
    public class MainViewModel : HasPopupViewModel, IOnLoadedHandler, IPopupable
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
        
        internal void GoHome()
        {
            CurrentPage = MainPage;
        }

        public ViewModelBase? CurrentPage
        {
            get => GetProperty<ViewModelBase?>();
            set => SetProperty(value);
        }
        private ViewModelBase LoginPage => App.ServiceProvider.GetRequiredService<LoginPageViewModel>();
        private ViewModelBase MainPage => App.ServiceProvider.GetRequiredService<MainPageViewModel>();
    }
}
