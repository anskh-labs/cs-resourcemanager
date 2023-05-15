using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class NoteContentViewModel : PageContentWithListViewModel<Note>
    {
        private readonly IDataServiceManager _dataServiceManager;
        public NoteContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.NoteDataService)
        {
            _dataServiceManager = dataServiceManager;

            MenuTitle = "Note";
            MenuIcon = AssetManager.Instance.GetImage("Note.png");
            Title = "Note Manager";
            PageColor = Constants.NotePageColor;

            _add_permission_name = Constants.ACTION_ADD_NOTE;
            _edit_permission_name = Constants.ACTION_EDIT_NOTE;
            _delete_permission_name = Constants.ACTION_DEL_NOTE;

            TagsCommand = new AsyncRelayCommand<Note>(OnTagsAsync);

            OnRefresh();
        }

        private async Task OnTagsAsync(Note note)
        {
            var vm = App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
            vm.Caption = string.Format("Manage tag for {0}", note.GetCaption());
            var tags = await _dataServiceManager.TagDataService.GetTagObjectForNoteID(note.ID);
            vm.Tags = tags.ToList();
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                await _dataServiceManager.NoteDataService.UpdateTags(note, vm.Tags);
                OnRefresh();
            }
        }

        protected override void OnEdit(Note note)
        {
            var vm = App.ServiceProvider.GetRequiredService<NotePopupViewModel>();
            vm.Caption = "Edit Note";
            vm.SetNote(note);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<NotePopupViewModel>();
            vm.Caption = "Add Note";
            var popup = ShowPopup(vm);
            if(popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnFilter()
        {
            Filter(FilterColumns.Note, "Filter Note");
        }

        public ICommand TagsCommand { get; }
    }
}
