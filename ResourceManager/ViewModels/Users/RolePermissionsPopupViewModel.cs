using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Security;
using ResourceManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class RolePermissionsPopupViewModel: PopupViewModelBase
    {
        public RolePermissionsPopupViewModel()
        {
            AddCommand = new RelayCommand<Permission>(AddPermission, x => SelectedPermission != null);
            RemoveCommand = new RelayCommand<Permission>(RemovePermission, x=>SelectedRolePermission != null);
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            Result = PopupResult.OK;
            OnClose();
        }

        private void RemovePermission(Permission perm)
        {
            RolePermissions = RolePermissions.Where(x => x.ID != perm.ID).ToList();
            Permissions = Permissions.Append(perm).ToList();
        }

        private void AddPermission(Permission perm)
        {
            RolePermissions = RolePermissions.Append(perm).ToList();
            Permissions = Permissions.Where(x => x.ID != perm.ID).ToList();
        }
        public IList<Permission> RolePermissions
        {
            get => GetProperty<IList<Permission>>();
            set => SetProperty(value);
        }
        public Permission SelectedRolePermission
        {
            get => GetProperty<Permission>();
            set => SetProperty(value);
        }
        public IList<Permission> Permissions
        {
            get => GetProperty<IList<Permission>>();
            set => SetProperty(value);
        }
        public Permission SelectedPermission
        {
            get => GetProperty<Permission>();
            set => SetProperty(value);
        }
        public bool CanEditRole
        {
            get
            {
                return AuthManager.User.IsInPermission("a_edit_role");
            }
        }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand SaveCommand { get; }
    }
}
