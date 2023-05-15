using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Security;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class NotePopupViewModel : PopupViewModel
    {
        private IWindowManager windowManager;
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool isEdit;

        public NotePopupViewModel(IDataServiceManager dataServiceManager, IWindowManager windowManager)
        {
            _dataServiceManager = dataServiceManager;
            this.windowManager = windowManager;
            isEdit = false;

            AddValidationRule(() => Title, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Title)));
            AddValidationRule(() => Notes, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Notes)));
            
            Date = DateTime.Now.Date;

            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
        }

        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetNote(Note note)
        {
            isEdit = true;
            ID = note.ID;
            Title = note.Title;
            Date = note.Date;
            Notes = note.Notes;
        }
        private async Task OnSaveAsync()
        {
            ValidateAllRules();

            if (!HasErrors)
            {
                try
                {
                    var note = new Note()
                    {
                        Title = Title,
                        Date = Date.Date,
                        Notes = Notes,
                        UserID = AuthManager.User.Identity.ID
                    };

                    if (isEdit)
                    {
                        await _dataServiceManager.NoteDataService.Update(ID, note);

                        Result = PopupResult.OK;
                    }
                    else
                    {
                        await _dataServiceManager.NoteDataService.Create(note);

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
        public DateTime Date
        {
            get => GetProperty<DateTime>();
            set => SetProperty(value);
        }
    
        public string Notes
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public ICommand SaveCommand { get; }
    }
}
