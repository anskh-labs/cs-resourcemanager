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
    public class EbookPopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool coverIsChanged = false;
        private bool fileIsChanged = false;
        private bool isEdit;
        private Book oldBook;

        public EbookPopupViewModel(IDataServiceManager dataServiceManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => Title, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Title)));
            AddValidationRule(() => Author, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Author)));
            AddValidationRule(() => FilePath, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(FilePath)));
            AddValidationRule(() => Publisher, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Publisher)));
            
            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
            BrowseFileCommand = new RelayCommand(OnBrowseFile);
            BrowseCoverCommand = new RelayCommand(OnBrowseCover);
        }
      
        private void OnBrowseCover()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Select cover file";
            dlg.Filter = App.Settings.EbookSettings.CoverFileExtensions;
            dlg.CheckFileExists = true;
            var res = dlg.ShowDialog();
            if(res!=null && res.Equals(true))
            {
                CoverPath = dlg.FileName;
                coverIsChanged = true;
            }
        }

        private void OnBrowseFile()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Select ebook file";
            dlg.Filter = App.Settings.EbookSettings.SupportExtensions;
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
        public void SetBook(Book book)
        {
            isEdit = true;
            oldBook = book;
            ID = book.ID;
            Title= book.Title;
            Author= book.Author;
            Publisher= book.Publisher;
            FilePath = book.FilePath;
            CoverPath = book.CoverPath;
            Abstraction = book.Abstraction;
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
                        string destPath = Path.Combine(App.Settings.EbookSettings.FolderPath, destFilename);
                        if (fileIsChanged)
                        {
                            if (App.Settings.EbookSettings.DeleteSourceFile)
                                File.Move(FilePath, destPath, true);
                            else
                                File.Copy(FilePath, destPath, true);
                        }
                        else
                        {
                            if (destFilename != oldBook.Filename)
                            {
                                File.Move(oldBook.FilePath, destPath);
                            }
                        }
                        string coverFilename = generatedFilename + Path.GetExtension(CoverPath);
                        string coverPath = Path.Combine(App.Settings.EbookSettings.CoverPath, coverFilename);
                        if (coverIsChanged)
                        {
                            if (!string.IsNullOrEmpty(CoverPath))
                            {
                                if (App.Settings.EbookSettings.DeleteSourceFile)
                                    File.Move(CoverPath, coverPath, true);
                                else
                                    File.Copy(CoverPath, coverPath, true);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(CoverPath))
                            {
                                if (coverFilename != oldBook.Cover)
                                {
                                    File.Move(CoverPath, coverPath);
                                }
                            }
                            else
                            {
                                coverFilename = string.Empty;
                            }
                        }
                        var book = new Book()
                        {
                            Title = Title,
                            Author = Author,
                            Publisher = Publisher,
                            Filename = destFilename,
                            Cover = coverFilename,
                            Abstraction = Abstraction
                        };
                        await _dataServiceManager.BookDataService.Update(ID, book);

                        Result = PopupResult.OK;

                    }
                    else
                    {
                        string destFilename = generatedFilename + Path.GetExtension(FilePath);
                        string destPath = Path.Combine(App.Settings.EbookSettings.FolderPath, destFilename);
                        if (App.Settings.EbookSettings.DeleteSourceFile)
                            File.Move(FilePath, destPath, true);
                        else
                            File.Copy(FilePath, destPath, true);
                        string coverFilename = string.Empty;
                        string coverPath = string.Empty;
                        if (!string.IsNullOrEmpty(CoverPath))
                        {
                            coverFilename = generatedFilename + Path.GetExtension(CoverPath);
                            coverPath = Path.Combine(App.Settings.EbookSettings.CoverPath, coverFilename);
                            if (App.Settings.EbookSettings.DeleteSourceFile)
                                File.Move(CoverPath, coverPath, true);
                            else
                                File.Copy(CoverPath, coverPath, true);
                        }
                        var book = new Book()
                        {
                            Title = Title,
                            Author = Author,
                            Publisher = Publisher,
                            Filename = destFilename,
                            Cover = coverFilename,
                            Abstraction = Abstraction
                        };
                        await _dataServiceManager.BookDataService.Create(book);

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
        public string CoverPath
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
