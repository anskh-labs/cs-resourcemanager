using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;

namespace NetCore.Mvvm.Abstractions
{
    public interface IPopupManager
    {
        T? ShowPopup<T>(T popupViewModel, IPopupable owner) where T : PopupViewModel;
        PopupResult ShowPopupMessage(string message, string caption, IPopupable owner, PopupButton popupButton = PopupButton.OK, PopupImage popupImage = PopupImage.None);
    }
}
