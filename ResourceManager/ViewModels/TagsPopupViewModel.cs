using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Mvvm.ViewModels;
using NetCore.Validators;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class TagsPopupViewModel : PopupViewModelBase
    {
        private readonly ITagDataService _tagDataService;
        public TagsPopupViewModel(ITagDataService tagDataService)
        {
            AddValidationRule(() => Name, TextValidator.Instance.Required, TextValidator.Instance.ErrorMessage("Required", "Tag Name"));

            _tagDataService = tagDataService;

            OKCommand = new RelayCommand(OnOk);
            AddCommand = new RelayCommand(OnAdd, CanAdd);
            RemoveCommand = new RelayCommand<Tag>(OnRemove);
        }

        private void OnRemove(Tag tag)
        {
            Tags = Tags.Where(x=>x.Name != tag.Name).ToList();
        }
        public string Name
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
 
        public IList<Tag> Tags
        { 
            get => GetProperty<IList<Tag>>();
            set => SetProperty(value);
        }
        private void OnOk()
        {
            Result = PopupResult.OK;
            OnClose();
        }
        private async void OnAdd()
        {
            ValidateAllRules();
            if (!HasErrors)
            {
                try
                {
                    var tags = Tags;
                    var tag = await _tagDataService.SingleOrDefault(x => x.Name.Equals(Name));
                    if (tag == null)
                    {
                        tag = await _tagDataService.Create(new Tag { Name = Name })!;
                    }
                    if (tags.SingleOrDefault(x => x.Name.Equals(Name)) == null)
                    {
                        tags.Add(tag);
                    }
                    Tags = tags.ToList();
                }
                catch(Exception e)
                {
                    windowManager.ShowMessageBox(e.Message, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        private bool CanAdd()
        {
            return !HasErrors;
        }

        public ICommand OKCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
    }
}