using NetCore.Mvvm.ViewModels;

namespace NetCore.Mvvm.Abstractions
{
    public interface IPopupable
    {
        PopupViewModel? ShowPoup(PopupViewModel popupViewModel);
        void HidePopup();
        PopupViewModel? CurrentPopup { get; set; }
        bool IsPopupVisible { get; set; }
    }
}
