using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class RolePopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool isEdit;
        public RolePopupViewModel(IDataServiceManager dataServiceManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => RoleName, text => TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Role Name"));
            AddValidationRule(() => RoleDescription, text => TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Role Description"));
            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
        }

        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetRole(Role role)
        {
            isEdit = true;
            ID = role.ID;
            RoleName = role.Name;
            RoleDescription = role.Description;
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
                        var editRole = new Role()
                        {
                            Name = RoleName,
                            Description = RoleDescription
                        };
                        var role = await _dataServiceManager.RoleDataService.Update(ID, editRole);
                        if (role != null)
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
                        var role = await _dataServiceManager.RoleDataService.Create(new Role() { Name = RoleName, Description = RoleDescription });
                        if (role != null)
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

        public string RoleName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string RoleDescription
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public ICommand SaveCommand { get; }
    }
}
