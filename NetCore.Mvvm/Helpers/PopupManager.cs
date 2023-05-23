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

        public T? ShowPopup<T>(T popupViewModel, IPopupable owner) where T : PopupViewModel
        {
            if(owner != null && owner is IPopupable)
            {
                return owner.ShowPoup(popupViewModel) as T;
            }
            return null;
        }

        public PopupResult ShowPopupMessage(string message, string caption, IPopupable owner, PopupButton popupButton = PopupButton.OK, PopupImage popupImage = PopupImage.None)
        {
            if (owner != null && owner is IPopupable)
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
