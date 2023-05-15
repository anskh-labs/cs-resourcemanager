using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using System.Windows.Input;

namespace NetCore.Mvvm.ViewModels
{
    public class PopupMessageViewModel : PopupViewModel
    {

        public PopupMessageViewModel()
            : base()
        {
            OKCommand = new RelayCommand(OnOK);
            YesCommand = new RelayCommand(OnYes);
            NoCommand = new RelayCommand(OnNo);
        }
        private void OnOK()
        {
            Result = PopupResult.OK;
            OnClose();
        }
        private void OnYes()
        {
            Result = PopupResult.Yes;
            OnClose();
        }
        private void OnNo()
        {
            Result = PopupResult.No;
            OnClose();
        }

        public string Message
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public PopupImage Image
        {
            get => GetProperty<PopupImage>();
            set => SetProperty(value);
        }
        public PopupButton Button
        {
            get => GetProperty<PopupButton>();
            set => SetProperty(value);
        }

        public ICommand OKCommand { get; }
        public ICommand YesCommand { get; }
        public ICommand NoCommand { get; }
    }
}
