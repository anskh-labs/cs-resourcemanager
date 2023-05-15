using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Validators;
using ResourceManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class FilterPopupViewModel : PopupViewModelBase
    {
        public FilterPopupViewModel()
        {
            AddValidationRule(() => SelectedColumn, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Filter column"));
            AddValidationRule(() => SelectedFilterOption, x => Enum.IsDefined(typeof(FilterOptions), x), TextValidator.Instance.ErrorMessage("Required", "Filter option"));
            AddValidationRule(() => FilterValue, ValidateFilterValue, TextValidator.Instance.ErrorMessage("Required", "Filter value"));

            FilterOptions = Enum.GetValues(typeof(FilterOptions)).Cast<FilterOptions>();

            OKCommand = new RelayCommand(OnOk, CanOk);
        }

        private bool ValidateFilterValue(string x)
        {
            if (IsFilterValueEnabled)
            {
                return TextValidator.Instance.Required(x);
            }

            return true;
        }

        public IEnumerable<string> Columns
        {
            get => GetProperty<IEnumerable<string>>();
            set => SetProperty(value);
        }
        public string SelectedColumn
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public bool IsFilterValueEnabled
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public IEnumerable<FilterOptions> FilterOptions
        {
            get => GetProperty<IEnumerable<FilterOptions>>();
            set => SetProperty(value);
        }
        public FilterOptions SelectedFilterOption
        {
            get => GetProperty<FilterOptions>();
            set
            {
                if(SetProperty(value))
                {
                    if(value == Helpers.FilterOptions.IsEmpty || value == Helpers.FilterOptions.IsNotEmpty)
                        IsFilterValueEnabled = false;
                    else
                        IsFilterValueEnabled = true;
                    OnPropertyChanged(nameof(FilterValue));
                }
            }
        }
        public string FilterValue
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public FilterParams FilterParams {get; private set;}
        private void OnOk()
        {
            ValidateAllRules();
            if(!HasErrors)
            {
                FilterParams = new FilterParams()
                {
                    ColumnName = SelectedColumn,
                    FilterOption = SelectedFilterOption,
                    FilterValue = FilterValue
                };
                Result = PopupResult.OK;
                OnClose();
            }
        }

        private bool CanOk()
        {
            return !HasErrors;
        }

        public ICommand OKCommand { get; }
    }
}
