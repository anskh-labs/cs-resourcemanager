using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Linq;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class PasswordContentViewModel : PageContentWithListViewModel<Password>
    {
        private readonly IDataServiceManager _dataServiceManager;
        public PasswordContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.PasswordDataService)
        {
            _dataServiceManager = dataServiceManager;
            MenuTitle = "Password";
            MenuIcon = AssetManager.Instance.GetImage("Passwords.png");
            Title = "Password Manager";
            PageColor = Constants.PasswordPageColor;

            _add_permission_name = Constants.ACTION_ADD_PASSWORD;
            _edit_permission_name = Constants.ACTION_EDIT_PASSWORD;
            _delete_permission_name = Constants.ACTION_DEL_PASSWORD;

            TagsCommand = new RelayCommand<Password>(OnTags);

            OnRefresh();
        }

        private async void OnTags(Password pass)
        {
            var vm = App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
            vm.Caption = string.Format("Manage tag for {0}", pass.GetCaption());
            var tags = await _dataServiceManager.TagDataService.GetTagObjectForPasswordID(pass.ID);
            vm.Tags = tags.ToList();
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                await _dataServiceManager.PasswordDataService.UpdateTags(pass, vm.Tags);
                OnRefresh();
            }
        }

        protected override void OnEdit(Password password)
        {
            var vm = App.ServiceProvider.GetRequiredService<PasswordPopupViewModel>();
            vm.Caption = "Edit Password";
            vm.SetPassword(password);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<PasswordPopupViewModel>();
            vm.Caption = "Add Password";
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }
        protected override void OnFilter()
        {
            Filter(FilterColumns.Password, "FIlter Password");
        }

        public ICommand TagsCommand { get; }
    }
}
