using Microsoft.Win32;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class RepositoryPopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private Repository oldRepository;
        private bool isEdit;
        private bool fileIsChanged = false;
        private BackgroundWorker bgWorker;
        string generatedFilename, destFilename, destPath;

        public RepositoryPopupViewModel(IDataServiceManager dataServiceManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => Title, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Title"));
            AddValidationRule(() => FilePath, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "File path"));
            
            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
            BrowseFileCommand = new RelayCommand(OnBrowseFile);

            IsOnProcess = false;
            bgWorker = new BackgroundWorker();
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        private void OnBrowseFile()
        {
            var dlg = new OpenFileDialog();
            dlg.Title = "Select repository file";
            dlg.Filter = App.Settings.RepositorySettings.SupportExtensions;
            dlg.CheckFileExists = true;
            var res = dlg.ShowDialog();
            if (res != null && res.Equals(true))
            {
                FilePath = dlg.FileName;
                fileIsChanged = true;
                if (!isEdit)
                {
                    Title = string.IsNullOrWhiteSpace(Title) ? Path.GetFileNameWithoutExtension(dlg.FileName) : Title;
                }
            }
        }

        private bool CanSave()
        {
            return !HasErrors && IsOnProcess == false;
        }
        public void SetRepo(Repository repo)
        {
            isEdit = true;
            oldRepository = repo;
            ID = repo.ID;
            Title= repo.Title;
            FilePath= repo.FilePath;
            FileType= repo.FileType;
            FileSize= repo.FileSize;
        }
        private Task OnSaveAsync()
        {
            ValidateAllRules();

            if (!HasErrors)
            {
                generatedFilename = string.Concat(Title.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
                if (generatedFilename.Length > 250) generatedFilename = generatedFilename.Substring(0, 250);
                destFilename = generatedFilename + Path.GetExtension(FilePath);
                destPath = Path.Combine(App.Settings.RepositorySettings.FolderPath, destFilename);

                try
                {
                    bgWorker.RunWorkerAsync();
                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }

            return Task.CompletedTask;
        }

        private void bgWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Result = PopupResult.OK;
            OnClose();
        }

        private async void bgWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => { IsOnProcess = true; });
            if (isEdit)
            {
                if (fileIsChanged)
                {
                    if (App.Settings.RepositorySettings.DeleteSourceFile)
                        File.Move(FilePath, destPath, true);
                    else
                        File.Copy(FilePath, destPath, true);
                }
                else
                {
                    if (destFilename != oldRepository.Filename)
                    {
                        File.Move(oldRepository.FilePath, destPath);
                    }
                }
                var repo = new Repository()
                {
                    Title = Title,
                    Filename = destFilename,
                    FileType = FileType,
                    FileSize = FileSize
                };
                await _dataServiceManager.RepositoryDataService.Update(ID, repo);
            }
            else
            {
                if (App.Settings.RepositorySettings.DeleteSourceFile)
                    File.Move(FilePath, destPath, true);
                else
                    File.Copy(FilePath, destPath, true);

                var repo = new Repository()
                {
                    Title = Title,
                    Filename = destFilename,
                    FileType = FileType,
                    FileSize = FileSize
                };
                await _dataServiceManager.RepositoryDataService.Create(repo);
            }
            Application.Current.Dispatcher.Invoke(() => { IsOnProcess = false; });
        }

        private void bgWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            
        }

        public string Title
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string FilePath
        {
            get => GetProperty<string>();
            set
            {
                if(SetProperty(value))
                {
                    if (File.Exists(value))
                    {
                        var info = new FileInfo(value);
                        FileType = info.Extension.Substring(1);
                        int size = (int)(info.Length >> 10);
                        FileSize = size + 1;
                    }
                }
            }
        }
        public string FileType
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public int FileSize
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public IList<Tag> Tags
        {
            get => GetProperty<IList<Tag>>();
            set => SetProperty(value);
        }
        public bool IsOnProcess
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public ICommand SaveCommand { get; }
        public ICommand BrowseFileCommand { get; }
    }
}
