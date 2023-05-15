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
    public class ArticlePageViewModel : PageViewModel
    {
        public ArticlePageViewModel()
        {

            PageTitle = "Article";
            PageIcon = AssetManager.Instance.GetImage("Articles.png");
            PageColor = Constants.ArticlePageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_ARTICLE_PERMISSION);

            var tagsContent = App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
            tagsContent.PageColor = PageColor;
            tagsContent.ArticleColumnWidth = 100;

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<ArticleContentViewModel>(),
                tagsContent,
                App.ServiceProvider.GetRequiredService<ArticleOptionsContentViewModel>()
            };
            
            SelectedItem = MenuItems.FirstOrDefault();
        }
    }
}
