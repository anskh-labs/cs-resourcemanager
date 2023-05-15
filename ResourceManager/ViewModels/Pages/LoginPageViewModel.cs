using NetCore.Cryptography;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Security;
using NetCore.Validators;
using ResourceManager.Helpers;
using ResourceManager.Services.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ResourceManager.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        public LoginPageViewModel(IDataServiceManager dataServiceManager)
        {
            _dataServiceManager = dataServiceManager;
            AddValidationRule(() => Username, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required","Username"));
            AddValidationRule(() => Password, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required","Password"));
            LoginCommand = new AsyncRelayCommand(LoginAsync, CanLogin);
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
        private bool CanLogin()
        {
            return !HasErrors;
        }
        public ICommand LoginCommand { get; }
        public ICommand HidePasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        private async Task LoginAsync()
        {
            ValidateAllRules();

            if (!HasErrors)
            {
                var loginUser = await _dataServiceManager.UserDataService.FirstOrDefault(x => x.AccountName == Username);

                if (loginUser != null)
                {
                    if (EncryptionManager.Instance.PasswordVerify(loginUser.Password, Password))
                    {

                        var resultRoles = await _dataServiceManager.RoleDataService.GetRoleStringForUserID(loginUser.ID);
                        var resultPermissions = await _dataServiceManager.PermissionDataService.GetPermissionForRoles(resultRoles);

                        AuthManager.User.Identity = new UserIdentity(loginUser.ID, loginUser.AccountName, resultRoles.ToArray(), resultPermissions.ToArray());

                        mainVM.RefreshUI();

                        Username = string.Empty;
                        Password = string.Empty;
                    }
                    else
                    {
                        ShowPopupMessage("Username or password invalid.", App.Settings.AppName, PopupButton.OK,PopupImage.Error);
                    }

                }
                else
                {
                    ShowPopupMessage("Username not found.", App.Settings.AppName, PopupButton.OK, PopupImage.Error);
                }
            }
        }

        public string Username
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Password
        {
            get => GetProperty<string>();
            set
            {
                if(SetProperty(value))
                {
                    OnPropertyChanged(() => PasswordNotEmpty);
                }
            }
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
                if(SetProperty(value))
                {
                    OnPropertyChanged(() => IsShowPassword);
                }
            }
        }
        public bool IsShowPassword => !IsHidePassword;
        public bool PasswordNotEmpty => Password?.Length > 0;
    }
}
