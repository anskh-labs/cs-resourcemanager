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
    public class EbookPageViewModel : PageViewModel
    {
        public EbookPageViewModel()
        {

            PageTitle = "Ebook";
            PageIcon = AssetManager.Instance.GetImage("Ebooks.png");
            PageColor = Constants.EbookPageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_EBOOK_PERMISSION);

            var tagsContent = App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
            tagsContent.PageColor = PageColor;
            tagsContent.EbookColumnWidth = 100;

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<EbookContentViewModel>(),
                tagsContent,
                App.ServiceProvider.GetRequiredService<EbookOptionsContentViewModel>(),
            };
            
            SelectedItem = MenuItems.FirstOrDefault();
        }
    }
}
