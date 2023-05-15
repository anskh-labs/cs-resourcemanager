using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class ActivityContentViewModel : PageContentWithListViewModel<Activity>
    {
        private readonly IDataServiceManager _dataServiceManager;
        public ActivityContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.ActivityDataService)
        {
            _dataServiceManager = dataServiceManager;

            MenuTitle = "Activity";
            MenuIcon = AssetManager.Instance.GetImage("Activity.png");
            Title = "Activity Manager";
            PageColor = Constants.ActivityPageColor;

            _add_permission_name = Constants.ACTION_ADD_ACTIVITY;
            _edit_permission_name = Constants.ACTION_EDIT_ACTIVITY;
            _delete_permission_name = Constants.ACTION_DEL_ACTIVITY;

            TagsCommand = new AsyncRelayCommand<Activity>(OnTagsAsync);

            OnRefresh();
        }

        private async Task OnTagsAsync(Activity activity)
        {
            var vm = App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
            vm.Caption = string.Format("Manage tag for {0}", activity.GetCaption());
            var tags = await _dataServiceManager.TagDataService.GetTagObjectForActivityID(activity.ID);
            vm.Tags = tags.ToList();
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                try
                {
                    await _dataServiceManager.ActivityDataService.UpdateTags(activity, vm.Tags);
                }
                catch (Exception ex)
                {
                    ShowPopupMessage(ex.Message, App.Settings.AppName, PopupButton.OK, PopupImage.Error);
                }
                OnRefresh();
            }
        }

        protected override void OnEdit(Activity activity)
        {
            var vm = App.ServiceProvider.GetRequiredService<ActivityPopupViewModel>();
            vm.Caption = "Edit Activity";
            vm.SetActivity(activity);
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            var vm = App.ServiceProvider.GetRequiredService<ActivityPopupViewModel>();
            vm.Caption = "Add Activity";
            var popup = ShowPopup(vm);
            if(popup?.Result == PopupResult.OK)
            {
                OnRefresh();
            }
        }

        protected override void OnFilter()
        {
            Filter(FilterColumns.Activity, "Filter Activity");
        }

        public ICommand TagsCommand { get; }
    }
}
