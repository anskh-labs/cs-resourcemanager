using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using System;

namespace ResourceManager.ViewModels
{
    public class PopupViewModelBase  : PopupViewModel
    {
        protected IPopupManager popupManager => App.ServiceProvider.GetRequiredService<IPopupManager>();
        protected IWindowManager windowManager => App.ServiceProvider.GetRequiredService<IWindowManager>();
        protected MainViewModel mainVM => App.ServiceProvider.GetRequiredService<MainViewModel>();
        protected IUiExecution UiExecution => App.ServiceProvider.GetRequiredService<IUiExecution>();
    }
}
