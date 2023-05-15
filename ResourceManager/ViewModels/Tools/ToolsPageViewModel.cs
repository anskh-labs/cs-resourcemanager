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
    public class ToolsPageViewModel : PageViewModel
    {
        public ToolsPageViewModel()
        {

            PageTitle = "Tools";
            PageIcon = AssetManager.Instance.GetImage("Tools.png");
            PageColor = Constants.ToolsPageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_TOOLS_PERMISSION);

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<DatabaseContentViewModel>()
            };
            SelectedItem = MenuItems.FirstOrDefault();
        }
    }
}
