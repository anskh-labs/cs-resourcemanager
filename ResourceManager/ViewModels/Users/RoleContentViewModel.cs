using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Linq;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class RoleContentViewModel : PageContentWithListViewModel<Role>
    {
        private IDataServiceManager _dataServiceManager;
        public RoleContentViewModel(IDataServiceManager dataServiceManager) 
            : base(dataServiceManager.RoleDataService)
        {
            _dataServiceManager = dataServiceManager;

            MenuTitle = "Roles";
            MenuIcon = AssetManager.Instance.GetImage("Roles.png");
            Title = "Role Manager";
            PageColor = Constants.UserPageColor;

            _edit_permission_name = Constants.ACTION_EDIT_ROLE;
            _add_permission_name = Constants.ACTION_ADD_ROLE;
            _delete_permission_name = Constants.ACTION_DEL_ROLE;

            RolePermissionsCommand = new RelayCommand<Role>(RolePermissionsAsync);

            OnRefresh();
        }

        private async void RolePermissionsAsync(Role role)
        {
            var vm = App.ServiceProvider.GetRequiredService<RolePermissionsPopupViewModel>();
            vm.Caption = "Manage Permission for role with name '" + role.Name + "'";
            var perms = await _dataServiceManager.PermissionDataService.GetAll();
            var rolePerms = await _dataServiceManager.PermissionDataService.GetPermissionObjectForRoleID(role.ID);
            foreach (var perm in rolePerms)
            {
                perms.Remove(perms.First(x => x.ID == perm.ID));
            }
            vm.Permissions = perms;
            vm.RolePermissions = rolePerms;
            var popup = ShowPopup(vm);
            if (popup != null && popup.Result == PopupResult.OK)
            {
                try
                {
                    int res = await _dataServiceManager.RoleDataService.UpdatePermissions(role, popup.RolePermissions);
                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                OnRefresh();
            }
        }

        protected override void OnEdit(Role role)
        {
            var vm = App.ServiceProvider.GetRequiredService<RolePopupViewModel>();
            vm.Caption = "Edit Role";
            vm.SetRole(role);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<RolePopupViewModel>();
            vm.Caption = "Add Role";
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }
        protected override void OnFilter()
        {
            Filter(FilterColumns.Role, "Filter Role");
        }
        public ICommand RolePermissionsCommand { get; }

    }
}
