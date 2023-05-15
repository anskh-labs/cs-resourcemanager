using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Security;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResourceManager.ViewModels
{
    public class UserPageViewModel : PageViewModel
    {
        public UserPageViewModel()
        {
            PageTitle = "User";
            PageIcon = AssetManager.Instance.GetImage("Users.png");
            PageColor = Constants.UserPageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_USER_PERMISSION);

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<UserContentViewModel>(),
                App.ServiceProvider.GetRequiredService<RoleContentViewModel>(),
                App.ServiceProvider.GetRequiredService<PermissionContentViewModel>()
            };

            SelectedItem = MenuItems.FirstOrDefault();
        }
        
    }
}
