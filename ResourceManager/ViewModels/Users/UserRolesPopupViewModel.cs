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
    public class UserRolesPopupViewModel: PopupViewModelBase
    {
        public UserRolesPopupViewModel()
        {
            AddCommand = new RelayCommand<Role>(AddRole, x => SelectedRole != null);
            RemoveCommand = new RelayCommand<Role>(RemoveRole, x=>SelectedUserRole != null);
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            Result = PopupResult.OK;
            OnClose();
        }

        private void RemoveRole(Role role)
        {
            UserRoles = UserRoles.Where(x => x.ID != role.ID).ToList();
            Roles = Roles.Append(role).ToList();
        }

        private void AddRole(Role role)
        {
            UserRoles = UserRoles.Append(role).ToList();
            Roles = Roles.Where(x => x.ID != role.ID).ToList();
        }
        public IList<Role> UserRoles
        {
            get => GetProperty<IList<Role>>();
            set => SetProperty(value);
        }
        public Role SelectedUserRole
        {
            get => GetProperty<Role>();
            set => SetProperty(value);
        }
        public IList<Role> Roles
        {
            get => GetProperty<IList<Role>>();
            set => SetProperty(value);
        }
        public Role SelectedRole
        {
            get => GetProperty<Role>();
            set => SetProperty(value);
        }
        public bool CanEditUser
        {
            get
            {
                return AuthManager.User.IsInPermission("a_edit_user");
            }
        }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand SaveCommand { get; }
    }
}
