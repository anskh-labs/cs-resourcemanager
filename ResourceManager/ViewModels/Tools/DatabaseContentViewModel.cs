using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using NetCore.DatabaseToolkit.SQLite;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Validators;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Settings;
using System;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class DatabaseContentViewModel : PageContentViewModel
    {
        private AppSettings appSettings => App.ServiceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
        public DatabaseContentViewModel()
        {
            MenuTitle = "Database";
            MenuIcon = AssetManager.Instance.GetImage("Database.png");
            Title = "Database Tools";
            PageColor = Constants.ToolsPageColor;

            AddValidationRule(() => FilePath, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(FilePath)));

            ExecuteCommand = new RelayCommand(OnExecute, CanExecute);
            OpenCommand = new RelayCommand(OnOpen);

            IsBackup = true;
        }

        private void OnOpen()
        {
            if (IsBackup)
            {
                var dlg = new SaveFileDialog();
                dlg.DefaultExt = ".bak";
                dlg.Filter = "Backup File|*.bak";
                dlg.Title = "Save file to backup";
                var b = dlg.ShowDialog();
                if (b != null & b.Equals(true))
                {
                    FilePath = dlg.FileName;
                }
            }
            else
            {
                var dlg = new OpenFileDialog();
                dlg.DefaultExt = ".bak";
                dlg.Filter = "Backup File|*.bak";
                dlg.Title = "Open file to restore";
                var b = dlg.ShowDialog();
                if (b != null & b.Equals(true))
                {
                    FilePath = dlg.FileName;
                }
            }
        }

        private bool CanExecute()
        {
            return !HasErrors;
        }

        private void OnExecute()
        {
            ValidateAllRules();
            if (!HasErrors)
            {
                try
                {
                    ISQLiteToolkit tools = App.ServiceProvider.GetRequiredService<ISQLiteToolkit>();
                    string dbFilename = Constants.DbFileName;
                    if (IsBackup)
                    {
                        int code;
                        string output;
                        (code, output) = tools.BackupDatabase(dbFilename, FilePath);
                        if (code == 0)
                        {
                            ShowPopupMessage("Database backed up succesfully.", appSettings.AppName, PopupButton.OK, PopupImage.Information);
                        }
                        else
                        {
                            ShowPopupMessage("Backup database failed. Message:" + output, appSettings.AppName, PopupButton.OK, PopupImage.Warning);
                        }
                    }
                    else
                    {
                        int code;
                        string output;
                        (code, output) = tools.RestoreDatabase(dbFilename, FilePath);
                        if (code == 0)
                        {
                            ShowPopupMessage("Database restored succesfully.", appSettings.AppName, PopupButton.OK, PopupImage.Information);
                        }
                        else
                        {
                            ShowPopupMessage("Restore database failed. Message:" + output, appSettings.AppName, PopupButton.OK, PopupImage.Warning);
                        }
                    }
                }
                catch (Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        public bool IsBackup
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public string FilePath
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public ICommand ExecuteCommand { get; }

        public ICommand OpenCommand { get; }
    }
}