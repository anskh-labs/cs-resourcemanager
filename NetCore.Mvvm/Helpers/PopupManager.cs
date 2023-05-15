using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using System;
using System.Linq;

namespace NetCore.Mvvm.Helpers
{
    public class PopupManager : IPopupManager
    {
        private readonly IServiceProvider _serviceProvider;
        private PopupMessageViewModel _popupMessageViewModel;
        public PopupManager(IServiceProvider serviceProvider, PopupMessageViewModel popupMessageViewModel)
        {
            _serviceProvider = serviceProvider;
            _popupMessageViewModel = popupMessageViewModel ?? _serviceProvider.GetRequiredService<PopupMessageViewModel>();
        }

        public T? ShowPopup<T>(IPopupable? owner, T popupViewModel) where T : PopupViewModel
        {
            owner = owner ?? (IPopupable)_serviceProvider.GetServices(typeof(IPopupable)).FirstOrDefault()!;
            if(owner != null)
            {
                 return (T)owner.ShowPoup(popupViewModel)!;
            }
            return null;
        }

        public PopupResult ShowPopupMessage(IPopupable? owner, string message, string caption, PopupButton popupButton, PopupImage popupImage)
        {
            owner = owner ?? (IPopupable)_serviceProvider.GetServices(typeof(IPopupable)).FirstOrDefault()!;
            if (owner != null)
            {
                _popupMessageViewModel.Message = message;
                _popupMessageViewModel.Caption = caption;
                _popupMessageViewModel.Button = popupButton;
                _popupMessageViewModel.Image = popupImage;

                var popup = owner.ShowPoup(_popupMessageViewModel);
                if (popup != null)
                    return popup.Result;

            }
            return PopupResult.None;
        }
    }
}
