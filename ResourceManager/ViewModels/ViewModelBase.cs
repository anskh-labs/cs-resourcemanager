using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using System;

namespace ResourceManager.ViewModels
{
    public class ViewModelBase : ValidationPropertyChangedBase
    {
        protected IPopupManager popupManager => App.ServiceProvider.GetRequiredService<IPopupManager>();
        protected IWindowManager windowManager => App.ServiceProvider.GetRequiredService<IWindowManager>();
        protected MainViewModel mainVM => App.ServiceProvider.GetRequiredService<MainViewModel>();
        protected IUiExecution UiExecution => App.ServiceProvider.GetRequiredService<IUiExecution>();

        public T? ShowPopup<T>(T popupViewModel) where T : PopupViewModel
        {
            return popupManager.ShowPopup(mainVM, popupViewModel);
        }
        public PopupResult ShowPopupMessage(string message, string caption, PopupButton popupButton = PopupButton.OK, PopupImage popupImage = PopupImage.None)
        {
            return popupManager.ShowPopupMessage(mainVM, message, caption, popupButton, popupImage);
        }
    }
}
