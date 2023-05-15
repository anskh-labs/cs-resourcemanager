using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Security;
using NetCore.Validators;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ResourceManager.ViewModels
{
    public class PasswordPopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool isEdit;

        public PasswordPopupViewModel(IDataServiceManager dataServiceManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => Name, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Name)));
            AddValidationRule(() => Username, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Username)));
            AddValidationRule(() => Pass, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Pass)));

            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);

            ShowPasswordCommand = new AsyncRelayCommand(ShowPassword);
            HidePasswordCommand = new AsyncRelayCommand(HidePasssword);
            ImageEye = AssetManager.Instance.GetImage("Hide.png");

            IsHidePassword = true;
        }
        private Task HidePasssword()
        {
            ImageEye = AssetManager.Instance.GetImage("Hide.png");
            IsHidePassword = true;

            return Task.CompletedTask;
        }

        private Task ShowPassword()
        {
            ImageEye = AssetManager.Instance.GetImage("Show.png");
            IsHidePassword = false;

            return Task.CompletedTask;
        }
        public BitmapImage ImageEye
        {
            get => GetProperty<BitmapImage>();
            set => SetProperty(value);
        }
        public bool IsHidePassword
        {
            get => GetProperty<bool>();
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(() => IsShowPassword);
                }
            }
        }
        public bool IsShowPassword => !IsHidePassword;
        public bool PasswordNotEmpty => Pass?.Length > 0;
        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetPassword(Password pass)
        {
            isEdit = true;

            ID = pass.ID;
            Name = pass.Name;
            Username = pass.Username;
            Pass = pass.Pass;
            Url = pass.Url;
            Description = pass.Description;
        }
        private async Task OnSaveAsync()
        {
            ValidateAllRules();

            if (!HasErrors)
            {
                try
                {
                    if (isEdit)
                    {
                        var editPassword = new Password()
                        {
                            Name = Name,
                            Username = Username,
                            Pass = Pass,
                            Url = Url,
                            Description = Description,
                            UserID = AuthManager.User.Identity.ID
                        };
                        var pass = await _dataServiceManager.PasswordDataService.Update(ID, editPassword);
                        Result = pass != null ? PopupResult.OK : PopupResult.None;
                    }
                    else
                    {
                        var pass = await _dataServiceManager.PasswordDataService.Create(new Password() { Name = Name, Username = Username, Pass = Pass, Url = Url, Description = Description, UserID = AuthManager.User.Identity.ID });
                        Result = pass != null ? PopupResult.OK : PopupResult.None;
                    }
                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                OnClose();
            }
        }

        public string Name
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Username
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Pass
        {
            get => GetProperty<string>();
            set
            {
                if (SetProperty(value))
                {
                    OnPropertyChanged(() => PasswordNotEmpty);
                }
            }
        }
        public string Url
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Description
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public ICommand SaveCommand { get; }
        public ICommand HidePasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
    }
}