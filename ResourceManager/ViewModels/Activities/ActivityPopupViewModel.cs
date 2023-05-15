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
    public class ActivityPopupViewModel : PopupViewModelBase
    {
        private readonly IDataServiceManager _dataServiceManager;
        private int ID;
        private bool isEdit;

        public ActivityPopupViewModel(IDataServiceManager dataServiceManager, IWindowManager windowManager)
        {
            _dataServiceManager = dataServiceManager;
            isEdit = false;

            AddValidationRule(() => Title, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Title)));
            AddValidationRule(() => Metric, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", nameof(Metric)));
            AddValidationRule(() => EndTime, x => Duration > 0, nameof(EndTime) + " must greater than start time.");
            AddValidationRule(() => Quantity, x => Quantity > 0, nameof(Quantity) + " must greater than 0.");

            DateTime now = DateTime.Now;
            Date = now.Date;
            StartTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            EndTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            SaveCommand = new AsyncRelayCommand(OnSaveAsync, CanSave);
        }

        private bool CanSave()
        {
            return !HasErrors;
        }
        public void SetActivity(Activity activity)
        {
            isEdit = true;
            ID = activity.ID;
            Title = activity.Title;
            Duration = activity.Duration;
            Quantity = activity.Quantity;
            Metric = activity.Metric;
            Date = activity.Date;
            StartTime = activity.StartTime;
            EndTime = activity.EndTime;
            Output = activity.Output;
            Note = activity.Note;
        }
        private async Task OnSaveAsync()
        {
            ValidateAllRules();

            if (!HasErrors)
            {
                try
                {
                    var act = new Activity()
                    {
                        Title = Title,
                        Duration = Duration,
                        Quantity = Quantity,
                        Metric = Metric,
                        Date = Date.Date,
                        StartTime = StartTime,
                        EndTime = EndTime,
                        Output = Output,
                        Note = Note,
                        UserID = AuthManager.User.Identity.ID
                    };

                    if (isEdit)
                    {
                        await _dataServiceManager.ActivityDataService.Update(ID, act);

                        Result = PopupResult.OK;
                    }
                    else
                    {
                        await _dataServiceManager.ActivityDataService.Create(act);

                        Result = PopupResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    windowManager.ShowMessageBox(ex.Message,System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);
                }
                OnClose();
            }
        }

        public string Title
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public int Quantity
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }
        public string Metric
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public DateTime Date
        {
            get => GetProperty<DateTime>();
            set
            {
                if (SetProperty(value))
                {
                    StartTime = new DateTime(value.Year, value.Month, value.Day, StartTime.Hour, StartTime.Minute, 0);
                    EndTime = new DateTime(value.Year, value.Month, value.Day, EndTime.Hour, EndTime.Minute, 0);
                }
            }
        }
        public int Duration
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }
        public DateTime StartTime
        {
            get => GetProperty<DateTime>();
            set
            {
                if(SetProperty(value))
                {
                    Duration = (int)(EndTime - StartTime).TotalMinutes;
                }
            }
        }
        public DateTime EndTime
        {
            get => GetProperty<DateTime>();
            set
            {
                if (SetProperty(value))
                {
                    Duration = (int)(EndTime - StartTime).TotalMinutes;
                }
            }
        }
        public string Output
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public string Note
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public ICommand SaveCommand { get; }
    }
}
