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

        public T? ShowPopup<T>(T popupViewModel, IPopupable? owner = null) where T : PopupViewModel
        {
            owner = owner ?? (IPopupable)_serviceProvider.GetServices(typeof(IPopupable)).FirstOrDefault()!;
            if(owner != null)
            {
                 return (T)owner.ShowPoup(popupViewModel)!;
            }
            return null;
        }

        public PopupResult ShowPopupMessage(string message, string caption = "", PopupButton popupButton = PopupButton.OK, PopupImage popupImage = PopupImage.None, IPopupable? owner = null)
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
