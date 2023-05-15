using Microsoft.Extensions.DependencyInjection;
using NetCore.Security;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResourceManager.ViewModels
{
    public class RepositoryPageViewModel : PageViewModel
    {
        public RepositoryPageViewModel()
        {

            PageTitle = "Repository";
            PageIcon = AssetManager.Instance.GetImage("Repository.png");
            PageColor = Constants.RepositoryPageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_REPOSITORY_PERMISSION);

            var tagsContent = App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
            tagsContent.PageColor = PageColor;
            tagsContent.RepositoryColumnWidth = 130;

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<RepositoryContentViewModel>(),
                tagsContent,
                App.ServiceProvider.GetRequiredService<RepositoryOptionsContentViewModel>()
            };

            SelectedItem = MenuItems.FirstOrDefault();
        }
    }
}
