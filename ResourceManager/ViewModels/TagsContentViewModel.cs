using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;
using System.Linq;

namespace ResourceManager.ViewModels
{
    public class TagsContentViewModel : PageContentWithListViewModel<Tag>
    {
        public TagsContentViewModel(IDataServiceManager dataServiceManager)
            : base(dataServiceManager.TagDataService)
        {
            MenuTitle = "Tag";
            MenuIcon = AssetManager.Instance.GetImage("Tag.png");
            Title = "Tag Manager";
            PageColor = Constants.DefaultPageColor;

            _add_permission_name = Constants.ACTION_ADD_TAG;
            _edit_permission_name = Constants.ACTION_EDIT_TAG;
            _delete_permission_name = Constants.ACTION_DEL_TAG;

            PasswordColumnWidth = 0;
            EbookColumnWidth = 0;
            ArticleColumnWidth = 0;
            ActivityColumnWidth = 0;
            RepositoryColumnWidth = 0;
            NoteColumnWidth = 0;
        }
        protected override async void OnRefresh()
        {
            _filter = FilterManager.Create<Tag>();
            if(PasswordColumnWidth > 0)
                _viewPager = new ViewPager<Tag>(await _dataService.GetWhere(x=>x.PasswordTags.Count>0), SelectedPageItem, SelectedRowsPerPage);
            else if (EbookColumnWidth > 0)
                _viewPager = new ViewPager<Tag>(await _dataService.GetWhere(x => x.BookTags.Count > 0), SelectedPageItem, SelectedRowsPerPage);
            else if (ArticleColumnWidth > 0)
                _viewPager = new ViewPager<Tag>(await _dataService.GetWhere(x => x.ArticleTags.Count > 0), SelectedPageItem, SelectedRowsPerPage);
            else if (ActivityColumnWidth > 0)
                _viewPager = new ViewPager<Tag>(await _dataService.GetWhere(x => x.ActivityTags.Count > 0), SelectedPageItem, SelectedRowsPerPage);
            else if (RepositoryColumnWidth > 0)
                _viewPager = new ViewPager<Tag>(await _dataService.GetWhere(x => x.RepositoryTags.Count > 0), SelectedPageItem, SelectedRowsPerPage);
            else if (NoteColumnWidth > 0)
                _viewPager = new ViewPager<Tag>(await _dataService.GetWhere(x => x.NoteTags.Count > 0), SelectedPageItem, SelectedRowsPerPage);
            ContentData = _viewPager.ViewContent;
            PageItems = _viewPager.PageItems;
            if (SelectedPageItem > PageItems.Count()) SelectedPageItem = PageItems.Count();
            PageInfo = string.Format("Page {0} of {1}", SelectedPageItem, PageItems.Count());
        }
        protected override void OnFilter()
        {
            Filter(FilterColumns.Tag, "Filter Tag");
        }
        
        public double PasswordColumnWidth
        {
            get => GetProperty<double>();
            set
            {
                if(SetProperty(value))
                {
                    if(value>0) OnRefresh();
                }
            }
        }
        public double ArticleColumnWidth
        {
            get => GetProperty<double>();
            set
            {
                if (SetProperty(value))
                {
                    if (value > 0) OnRefresh();
                }
            }
        }
        public double EbookColumnWidth
        {
            get => GetProperty<double>();
            set
            {
                if (SetProperty(value))
                {
                    if (value > 0) OnRefresh();
                }
            }
        }
        public double RepositoryColumnWidth
        {
            get => GetProperty<double>();
            set
            {
                if (SetProperty(value))
                {
                    if (value > 0) OnRefresh();
                }
            }
        }
        public double ActivityColumnWidth
        {
            get => GetProperty<double>();
            set
            {
                if (SetProperty(value))
                {
                    if (value > 0) OnRefresh();
                }
            }
        }
        public double NoteColumnWidth
        {
            get => GetProperty<double>();
            set
            {
                if (SetProperty(value))
                {
                    if (value > 0) OnRefresh();
                }
            }
        }
    }
}
