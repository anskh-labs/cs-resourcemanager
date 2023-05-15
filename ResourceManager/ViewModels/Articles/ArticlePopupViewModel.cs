using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using ResourceManager.Settings;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class ArticlePopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool fileIsChanged = false;
        private bool isEdit;
        private Article oldArticle;

        public ArticlePopupViewModel(IDataServiceManager dataServiceManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => Title, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Title"));
            AddValidationRule(() => Author, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Author"));
            AddValidationRule(() => FilePath, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "File Path"));

            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
            BrowseFileCommand = new RelayCommand(OnBrowseFile);
        }


        private void OnBrowseFile()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Select article file";
            dlg.Filter = App.Settings.ArticleSettings.SupportExtensions;
            dlg.CheckFileExists = true;
            var res = dlg.ShowDialog();
            if (res != null && res.Equals(true))
            {
                FilePath = dlg.FileName;
                fileIsChanged = true;
                if (!isEdit)
                {
                    Title = string.IsNullOrWhiteSpace(Title) ? Path.GetFileNameWithoutExtension(dlg.FileName) : Title;
                    Author = string.IsNullOrWhiteSpace(Author) ? "-" : string.Empty;
                    Publisher = string.IsNullOrWhiteSpace(Publisher) ? "-" : string.Empty;
                }
            }
        }

        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetArticle(Article article)
        {
            isEdit = true;
            oldArticle = article;
            ID = article.ID;
            Title = article.Title;
            Author = article.Author;
            FilePath = article.FilePath;
        }
        private async Task OnSaveAsync()
        {
            ValidateAllRules();

            if (!HasErrors)
            {
                string generatedFilename = string.Concat(Title.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
                if (generatedFilename.Length > 250) generatedFilename = generatedFilename.Substring(0, 250);
                try
                {
                    if (isEdit)
                    {
                        string destFilename = generatedFilename + Path.GetExtension(FilePath);
                        string destPath = Path.Combine(App.Settings.ArticleSettings.FolderPath, destFilename);
                        if (fileIsChanged)
                        {
                            if (App.Settings.ArticleSettings.DeleteSourceFile)
                                File.Move(FilePath, destPath, true);
                            else
                                File.Copy(FilePath, destPath, true);
                        }
                        else
                        {
                            if (destFilename != oldArticle.Filename)
                            {
                                File.Move(oldArticle.FilePath, destPath);
                            }
                        }
                        var article = new Article()
                        {
                            Title = Title,
                            Author = Author,
                            Filename = destFilename
                        };
                        await _dataServiceManager.ArticleDataService.Update(ID, article);

                        Result = PopupResult.OK;


                    }
                    else
                    {

                        string destFilename = generatedFilename + Path.GetExtension(FilePath);
                        string destPath = Path.Combine(App.Settings.ArticleSettings.FolderPath, destFilename);
                        if (App.Settings.ArticleSettings.DeleteSourceFile)
                            File.Move(FilePath, destPath, true);
                        else
                            File.Copy(FilePath, destPath, true);

                        var article = new Article()
                        {
                            Title = Title,
                            Author = Author,
                            Filename = destFilename
                        };
                        await _dataServiceManager.ArticleDataService.Create(article);

                        Result = PopupResult.OK;
                    }


                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                OnClose();
            }
        }

        public string Title
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Author
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Publisher
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string FilePath
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Cover
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Abstraction
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public ICommand SaveCommand { get; }
        public ICommand BrowseFileCommand { get; }
        public ICommand BrowseCoverCommand { get; }
    }
}
