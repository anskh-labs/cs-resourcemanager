using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services;
using ResourceManager.Services.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class RepositoryContentViewModel : PageContentWithListViewModel<Repository>
    {
        private readonly IDataServiceManager _dataServiceManager;
        public RepositoryContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.RepositoryDataService)
        {
            _dataServiceManager = dataServiceManager;
            MenuTitle = "Repository";
            MenuIcon = AssetManager.Instance.GetImage("Repository.png");
            Title = "Repository Manager";
            PageColor = Constants.RepositoryPageColor;

            _edit_permission_name = Constants.ACTION_EDIT_REPOSITORY;
            _add_permission_name = Constants.ACTION_ADD_REPOSITORY;
            _delete_permission_name = Constants.ACTION_DEL_REPOSITORY;

            TagsCommand = new AsyncRelayCommand<Repository>(OnTagsAsync);
            ExploreCommand = new RelayCommand<Repository>(OnExplore);

            OnRefresh();
        }

        private void OnExplore(Repository repo)
        {
            string filename = repo.FilePath;

            if (!File.Exists(filename))
            {
                ShowPopupMessage("File '" + filename + "' doesn't exists.", App.Settings.AppName, PopupButton.OK, PopupImage.Error);
                return;
            }
            var process = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "explorer.exe";

            // sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $@"/select,""{filename}""";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        private async Task OnTagsAsync(Repository repo)
        {
            var vm = App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
            vm.Caption = string.Format("Manage tag for {0}", repo.GetCaption());
            var tags = await _dataServiceManager.TagDataService.GetTagObjectForRepositoryID(repo.ID);
            vm.Tags = tags.ToList();
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                await _dataServiceManager.RepositoryDataService.UpdateTags(repo, vm.Tags);
                OnRefresh();
            }
        }


        protected override void OnEdit(Repository repo)
        {
            var vm = App.ServiceProvider.GetRequiredService<RepositoryPopupViewModel>();
            vm.Caption = "Edit Repository";
            vm.SetRepo(repo);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<RepositoryPopupViewModel>();
            vm.Caption = "Add Repository";
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }
        protected override void OnFilter()
        {
            Filter(FilterColumns.Repository, "Filter Repository");
        }

        public ICommand TagsCommand { get; }
        public ICommand ExploreCommand { get; }
    }
}
