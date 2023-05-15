using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;

namespace NetCore.Mvvm.Abstractions
{
    public interface IPopupManager
    {
        T? ShowPopup<T>(IPopupable owner, T popupViewModel) where T : PopupViewModel;
        PopupResult ShowPopupMessage(IPopupable owner, string message, string caption, PopupButton popupButton = PopupButton.OK, PopupImage popupImage = PopupImage.None);
    }
}
