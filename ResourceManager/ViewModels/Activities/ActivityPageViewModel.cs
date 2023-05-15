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
    public class ActivityPageViewModel : PageViewModel
    {
        public ActivityPageViewModel()
        {

            PageTitle = "Activity";
            PageIcon = AssetManager.Instance.GetImage("Activity.png");
            PageColor = Constants.ActivityPageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_ACTIVITY_PERMISSION);

            var tagsContent = App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
            tagsContent.PageColor = PageColor;
            tagsContent.ActivityColumnWidth = 110;

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<ActivityContentViewModel>(),
                tagsContent
            };

            SelectedItem = MenuItems.FirstOrDefault();
        }
    }
}
