using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;

namespace NetCore.Mvvm.Abstractions
{
    public interface IPopupManager
    {
        T? ShowPopup<T>(T popupViewModel, IPopupable? owner = null) where T : PopupViewModel;
        PopupResult ShowPopupMessage(string message, string caption = "", PopupButton popupButton = PopupButton.OK, PopupImage popupImage = PopupImage.None, IPopupable? owner = null);
    }
}
