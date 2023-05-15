using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class PermissionContentViewModel : PageContentWithListViewModel<Permission>
    {
        
        public PermissionContentViewModel(IPermissionDataService dataService)
            : base(dataService)
        {
            MenuTitle = "Permissions";
            MenuIcon = AssetManager.Instance.GetImage("Permissions.png");
            Title = "Permission Manager";
            PageColor = Constants.UserPageColor;

            _edit_permission_name = Constants.ACTION_EDIT_PERMISSION;
            _add_permission_name = Constants.ACTION_ADD_PERMISSION;
            _delete_permission_name = Constants.ACTION_DEL_PERMISSION;

            OnRefresh();
        }
        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<PermissionPopupViewModel>();
            vm.Caption = "Add Permission";
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnEdit(Permission perm)
        {
            var vm = App.ServiceProvider.GetRequiredService<PermissionPopupViewModel>();
            vm.Caption = "Edit Permission";
            vm.SetPermission(perm);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnFilter()
        {
            Filter(FilterColumns.Permission, "Filter Permission");
        }
        public ICommand RolePermissionsCommand { get; }
    }
}
