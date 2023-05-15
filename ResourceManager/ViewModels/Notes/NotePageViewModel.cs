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
    public class NotePageViewModel : PageViewModel
    {
        public NotePageViewModel()
        {

            PageTitle = "Note";
            PageIcon = AssetManager.Instance.GetImage("Note.png");
            PageColor = Constants.NotePageColor;
            HasPermission = AuthManager.User.IsInPermission(Constants.VIEW_NOTE_PERMISSION);

            var tagsContent = App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
            tagsContent.PageColor = PageColor;
            tagsContent.NoteColumnWidth = 110;

            MenuItems = new ObservableCollection<PageContentViewModel>
            {
                App.ServiceProvider.GetRequiredService<NoteContentViewModel>(),
                tagsContent
            };

            SelectedItem = MenuItems.FirstOrDefault();
        }
    }
}
