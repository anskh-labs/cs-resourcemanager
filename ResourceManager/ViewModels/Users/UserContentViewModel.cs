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
    public class UserContentViewModel : PageContentWithListViewModel<User>
    {
        private readonly IDataServiceManager _dataServiceManager;

        public UserContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.UserDataService)
        {
            _dataServiceManager = dataServiceManager;

            MenuTitle = "User";
            MenuIcon = AssetManager.Instance.GetImage("Users.png");
            Title = "User Manager";
            PageColor = Constants.UserPageColor;
            RolesUserCommand = new RelayCommand<User>(RolesUser);

            _add_permission_name = Constants.ACTION_ADD_USER;
            _edit_permission_name = Constants.ACTION_EDIT_USER;
            _delete_permission_name = Constants.ACTION_DEL_USER;

            OnRefresh();
        }

        private async void RolesUser(User user)
        {
            var vm = App.ServiceProvider.GetRequiredService<UserRolesPopupViewModel>();
            vm.Caption = "Manage Role for user with account name '" + user.AccountName + "'";
            var roles = await _dataServiceManager.RoleDataService.GetAll();
            var userRoles = await _dataServiceManager.RoleDataService.GetRoleObjectForUserID(user.ID);
            foreach (var role in userRoles)
            {
                roles.Remove(roles.First(x => x.ID == role.ID));
            }
            vm.Roles = roles;
            vm.UserRoles = userRoles;
            var popup = ShowPopup(vm);
            if (popup != null && popup.Result == PopupResult.OK)
            {
                try
                {
                    int res = await _dataServiceManager.UserDataService.UpdateRoles(user, popup.UserRoles);
                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                OnRefresh();
            }
        }

        protected override void OnEdit(User user)
        {
            var vm = App.ServiceProvider.GetRequiredService<UserPopupViewModel>();
            vm.Caption = "Edit User";
            vm.SetUser(user);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<UserPopupViewModel>();
            vm.Caption = "Add User";
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }
        protected override void OnFilter()
        {
            Filter(FilterColumns.User, "Filter User");
        }
        public ICommand RolesUserCommand { get; }
    }
}
