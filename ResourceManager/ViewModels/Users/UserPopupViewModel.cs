using NetCore.Cryptography;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class UserPopupViewModel : PopupViewModelBase
    {
        private readonly IUserDataService _userDataService;
        private int ID;
        private bool isEdit;

        public UserPopupViewModel(IUserDataService userDataService)
        {
            _userDataService = userDataService;
            isEdit = false;

            AddValidationRule(() => RealName, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Real Name"));
            AddValidationRule(() => Username, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Username"));
            AddValidationRule(() => Password, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Password"));
            AddValidationRule(() => RepeatPassword, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Repeat Password"));
            AddValidationRule(() => RepeatPassword, x => Password == RepeatPassword, "Repeat Password must be equal to Password");
            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
        }

        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetUser(User user)
        {
            isEdit = true;
            ID = user.ID;
            RealName = user.Name;
            Username = user.AccountName;
            Password = EncryptionManager.Instance.DecryptString(user.Password, EncryptionManager.KEY);
            RepeatPassword = Password;
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
                        var editUser = new User()
                        {
                            Name = RealName,
                            AccountName = Username,
                            Password = EncryptionManager.Instance.EncryptString(Password, EncryptionManager.KEY)
                        };
                        var user = await _userDataService.Update(ID, editUser);
                        if (user != null)
                        {
                            Result = PopupResult.OK;
                        }
                        else
                        {
                            Result = PopupResult.None;
                        }
                    }
                    else
                    {
                        var existUser = (await _userDataService.GetWhere(x => x.Name.Equals(RealName) || x.AccountName.Equals(Username))).SingleOrDefault();
                        if (existUser != null)
                        {

                        }

                        var user = await _userDataService.Create(new User() { Name = RealName, AccountName = Username, Password = EncryptionManager.Instance.EncryptString(Password, EncryptionManager.KEY) });
                        if (user != null)
                        {
                            Result = PopupResult.OK;
                        }
                        else
                        {
                            Result = PopupResult.None;
                        }
                    }
                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                OnClose();
            }
        }
        
        public string RealName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Username
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Password
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string RepeatPassword
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public ICommand SaveCommand { get; }
    }
}
