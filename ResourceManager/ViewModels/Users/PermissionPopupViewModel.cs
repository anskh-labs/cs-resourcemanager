using Microsoft.Extensions.Options;
using NetCore.Cryptography;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using ResourceManager.Settings;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class PermissionPopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool isEdit;

        public PermissionPopupViewModel(IServiceProvider serviceProvider, IOptions<AppSettings> settings, IDataServiceManager dataServiceManager, IWindowManager windowManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => PermissionName, text => TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Permission Name"));
            AddValidationRule(() => PermissionDescription, text => TextValidator.Instance.Required(text), TextValidator.Instance.ErrorMessage("Required", "Permission Description"));
            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
        }

        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetPermission(Permission perm)
        {
            isEdit = true;
            ID = perm.ID;
            PermissionName = perm.Name;
            PermissionDescription = perm.Description;
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
                        var editPerm = new Permission()
                        {
                            Name = PermissionName,
                            Description = PermissionDescription
                        };
                        var perm = await _dataServiceManager.PermissionDataService.Update(ID, editPerm);
                        if (perm != null)
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
                        var perm = await _dataServiceManager.PermissionDataService.Create(new Permission() { Name = PermissionName, Description = PermissionDescription });
                        if (perm != null)
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

        public string PermissionName
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string PermissionDescription
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public ICommand SaveCommand { get; }
    }
}
