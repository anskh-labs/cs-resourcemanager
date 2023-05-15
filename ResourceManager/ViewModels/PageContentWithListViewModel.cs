using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Mvvm.Controls;
using NetCore.Security;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public abstract class PageContentWithListViewModel<T> : PageContentViewModel where T : BaseEntity
    {
        protected ViewPager<T> _viewPager;
        protected Filter<T> _filter;
        protected IDataService<T> _dataService;
        protected string _add_permission_name;
        protected string _delete_permission_name;
        protected string _edit_permission_name;
        public PageContentWithListViewModel(IDataService<T> dataService)
        {
            _dataService = dataService;
            
            RefreshCommand = new RelayCommand(OnRefresh);
            FilterCommand = new RelayCommand(OnFilter);
            DeleteCommand = new RelayCommand<T>(OnDelete);
            AddCommand = new RelayCommand(OnAdd);
            EditCommand = new RelayCommand<T>(OnEdit);
            FirstCommand = new RelayCommand(OnFirst);
            PrevCommand = new RelayCommand(OnPrev);
            NextCommand = new RelayCommand(OnNext);
            LastCommand = new RelayCommand(OnLast);

            RowsPerPage = new List<string>(new string[] { "25", "50", "all" });
            PageItems = new List<int>(new int[] { 1 });
            SelectedRowsPerPage = RowsPerPage.First();
            SelectedPageItem = PageItems.First();
        }
        protected void OnLast()
        {
            if (SelectedPageItem < PageItems.Count())
            {
                SelectedPageItem = PageItems.Count();
            }
        }
        protected void OnNext()
        {
            if (SelectedPageItem < PageItems.Count())
            {
                SelectedPageItem++;
            }
        }
        protected void OnPrev()
        {
            if (SelectedPageItem > 1)
            {
                SelectedPageItem--;
            }
        }
        protected void OnFirst()
        {
            if (SelectedPageItem > 1)
            {
                SelectedPageItem = 1;
            }
        }
        protected virtual void OnEdit(T t) { }
        protected virtual void OnAdd() { }
        protected abstract void OnFilter();
        protected virtual void Filter(IEnumerable<string> columns, string caption)
        {
            var vm = App.ServiceProvider.GetRequiredService<FilterPopupViewModel>();
            vm.Caption = caption;
            vm.Columns = columns;
            var popup = ShowPopup(vm);
            if (popup?.Result == PopupResult.OK)
            {
                _viewPager = new ViewPager<T>(_filter.FilteredData(vm.FilterParams, _viewPager.Contents), SelectedPageItem, SelectedRowsPerPage);
                SelectedPageItem = _viewPager.SelectedPageItem;
                ContentData = _viewPager.ViewContent;
                PageItems = _viewPager.PageItems;
                PageInfo = string.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count());
            }
        }

        protected virtual void OnDelete(T t)
        {
            var result = ShowPopupMessage("Delete " + t.GetCaption() + "?", "Confirmation", PopupButton.YesNo, PopupImage.Question);
            if (result == PopupResult.Yes)
            {
                _dataService.Delete(t.ID);
                OnRefresh();
            }
        }
        protected virtual async void OnRefresh()
        {
            _filter = FilterManager.Create<T>();
            _viewPager = new ViewPager<T>(await _dataService.GetAll(), SelectedPageItem, SelectedRowsPerPage);
            ContentData = _viewPager.ViewContent;
            PageItems = _viewPager.PageItems;
            if (SelectedPageItem > PageItems.Count()) SelectedPageItem = PageItems.Count();
            PageInfo = string.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count());
        }

        public IEnumerable<int> PageItems
        {
            get => GetProperty<IEnumerable<int>>();
            set => SetProperty(value);
        }
        public int SelectedPageItem
        {
            get => GetProperty<int>();
            set
            {
                if (SetProperty(value))
                {
                    if (_viewPager != null)
                    {
                        ContentData = _viewPager.Goto(value, SelectedRowsPerPage);
                        PageInfo = string.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count());
                    }
                }
            }
        }
        public IList<string> RowsPerPage
        {
            get => GetProperty<IList<string>>();
            set => SetProperty(value);
        }
        public string SelectedRowsPerPage
        {
            get => GetProperty<string>();
            set
            {
                if (SetProperty(value))
                {
                    if (_viewPager != null)
                    {
                        ContentData = _viewPager.Goto(SelectedPageItem, value);
                        PageItems = _viewPager.PageItems;
                        if (SelectedPageItem > PageItems.Count()) SelectedPageItem = PageItems.Count();
                        PageInfo = string.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count());
                    }
                }
            }
        }
        public IEnumerable<T> ContentData
        {
            get => GetProperty<IEnumerable<T>>();
            set => SetProperty(value);
        }
        public T SelectedData
        {
            get => GetProperty<T>();
            set => SetProperty(value);
        }
        public string PageInfo
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public bool CanAdd => AuthManager.User.IsInPermission(_add_permission_name);

        public bool CanEdit => AuthManager.User.IsInPermission(_edit_permission_name);

        public bool CanDelete => AuthManager.User.IsInPermission(_delete_permission_name);

        public ICommand AddCommand { get; protected set; }
        public ICommand FilterCommand { get; protected set; }
        public ICommand RefreshCommand { get; protected set; }
        public ICommand EditCommand { get; protected set; }
        public ICommand DeleteCommand { get; protected set; }
        public ICommand FirstCommand { get; protected set; }
        public ICommand PrevCommand { get; protected set; }
        public ICommand NextCommand { get; protected set; }
        public ICommand LastCommand { get; protected set; }
    }
}
