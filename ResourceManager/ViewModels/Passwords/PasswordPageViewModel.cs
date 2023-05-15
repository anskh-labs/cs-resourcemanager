using Microsoft.Extensions.DependencyInjection;
using NetCore.Security;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResourceManager.ViewModels
{
    public class PasswordPageViewModel : PageViewModel
    {
        public PasswordPageViewModel()
        {
            PageTitle = "Password";
            PageIcon = AssetManager.Instance.GetImage("Passwords.png");
            PageColor = Constants.PasswordPageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_PASSWORD_PERMISSION);

            var tagsContent = App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
            tagsContent.PageColor= PageColor;
            tagsContent.PasswordColumnWidth = 120;

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<PasswordContentViewModel>(),
                tagsContent
            };
            
            SelectedItem = MenuItems.FirstOrDefault();
            
        }
    }
}
