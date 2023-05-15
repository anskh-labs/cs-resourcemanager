using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class ArticleContentViewModel : PageContentWithListViewModel<Article>
    {
        private IDataServiceManager _dataServiceManager;
        public ArticleContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.ArticleDataService)
        {
            _dataServiceManager = dataServiceManager;

            MenuTitle = "Article";
            MenuIcon = AssetManager.Instance.GetImage("Articles.png");
            Title = "Article Manager";
            PageColor = Constants.ArticlePageColor;

            _add_permission_name = Constants.ACTION_ADD_ARTICLE;
            _edit_permission_name = Constants.ACTION_EDIT_ARTICLE;
            _delete_permission_name = Constants.ACTION_DEL_ARTICLE;

            TagsCommand = new AsyncRelayCommand<Article>(OnTagsAsync);
            OpenCommand = new RelayCommand<Article>(OnOpen);

            OnRefresh();
        }

        private void OnOpen(Article article)
        {
            string filename = article.FilePath;

            if (!File.Exists(filename))
            {
                ShowPopupMessage("File '" + filename + "' doesn't exists.", App.Settings.AppName, PopupButton.OK, PopupImage.Error);
                return;
            }
            try
            {
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
            catch(Exception ex)
            {
                ShowPopupMessage(ex.Message, App.Settings.AppName, PopupButton.OK, PopupImage.Error);
            }
        }

        private async Task OnTagsAsync(Article article)
        {
            var vm = App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
            vm.Caption = string.Format("Manage tag for {0}", article.GetCaption());
            var tags = await _dataServiceManager.TagDataService.GetTagObjectForArticleID(article.ID);
            vm.Tags = tags.ToList();
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                try
                {
                    await _dataServiceManager.ArticleDataService.UpdateTags(article, vm.Tags);
                }
                catch (Exception ex)
                {
                    ShowPopupMessage(ex.Message, App.Settings.AppName, PopupButton.OK, PopupImage.Error);
                }
                OnRefresh();
            }
        }

        protected override void OnEdit(Article article)
        {
            var vm = App.ServiceProvider.GetRequiredService<ArticlePopupViewModel>();
            vm.Caption = "Edit Article";
            vm.SetArticle(article);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<ArticlePopupViewModel>();
            vm.Caption = "Add Article";
            var popup = ShowPopup(vm);
            if(popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnFilter()
        {
            Filter(FilterColumns.Article, "Filter Article");
        }

        public ICommand TagsCommand { get; }
        public ICommand OpenCommand { get; }
    }
}
