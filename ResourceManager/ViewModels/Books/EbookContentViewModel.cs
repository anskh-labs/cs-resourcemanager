using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services;
using ResourceManager.Services.Abstractions;
using ResourceManager.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class EbookContentViewModel : PageContentWithListViewModel<Book>
    {
        private IDataServiceManager _dataServiceManager;
        public EbookContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.BookDataService)
        {
            _dataServiceManager = dataServiceManager;

            MenuTitle = "Ebook";
            MenuIcon = AssetManager.Instance.GetImage("Ebooks.png");
            Title = "Ebook Manager";
            PageColor = Constants.EbookPageColor;

            _edit_permission_name = Constants.ACTION_EDIT_EBOOK;
            _add_permission_name = Constants.ACTION_ADD_EBOOK;
            _delete_permission_name = Constants.ACTION_DEL_EBOOK;

            TagsCommand = new AsyncRelayCommand<Book>(OnTagsAsync);
            OpenCommand = new RelayCommand<Book>(OnOpen);

            OnRefresh();
        }

        private void OnOpen(Book book)
        {
            string filename = book.FilePath;
            if (!File.Exists(filename))
            {
                ShowPopupMessage("File '" + filename + "' doesn't exists.", App.Settings.AppName, PopupButton.OK, PopupImage.Error);
                return;
            }
            var process = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";

            // sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $@"/c ""{filename}""";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            //process.WaitForExit();
            //process.Close();
            process.Dispose();
        }

        private async Task OnTagsAsync(Book book)
        {
            var vm = App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
            vm.Caption = string.Format("Manage tag for {0}", book.GetCaption());
            var tags = await _dataServiceManager.TagDataService.GetTagObjectForBookID(book.ID);
            vm.Tags = tags.ToList();
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                await _dataServiceManager.BookDataService.UpdateTags(book, vm.Tags);
                OnRefresh();
            }
        }

        protected override void OnEdit(Book book)
        {
            var vm = App.ServiceProvider.GetRequiredService<EbookPopupViewModel>();
            vm.Caption = "Edit Book";
            vm.SetBook(book);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<EbookPopupViewModel>();
            vm.Caption = "Add Book";
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }
        protected override void OnDelete(Book book)
        {
            var result = ShowPopupMessage("Delete " + book.GetCaption() + "?", "Confirmation", PopupButton.YesNo, PopupImage.Question);
            if (result == PopupResult.Yes)
            {
                _dataService.Delete(book.ID);
                string filename = Path.Combine(App.Settings.ArticleSettings.FolderPath, book.Filename);
                if(File.Exists(filename))
                    File.Delete(filename);

                if (File.Exists(book.Cover))
                    File.Delete(book.Cover);
                OnRefresh();
            }
        }

        protected override void OnFilter()
        {
            Filter(FilterColumns.Book, "Filter Book");
        }

        public ICommand TagsCommand { get; }
        public ICommand OpenCommand { get; }
    }
}
