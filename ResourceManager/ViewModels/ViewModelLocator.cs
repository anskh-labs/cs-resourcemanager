using Microsoft.Extensions.DependencyInjection;
using System;

namespace ResourceManager.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
        public MainPageViewModel MainPageViewModel => App.ServiceProvider.GetRequiredService<MainPageViewModel>();
        public LoginPageViewModel LoginPageViewModel => App.ServiceProvider.GetRequiredService<LoginPageViewModel>();
        public UserPageViewModel UserPageViewModel => App.ServiceProvider.GetRequiredService<UserPageViewModel>();
        public UserContentViewModel UserContentViewModel => App.ServiceProvider.GetRequiredService<UserContentViewModel>();
        public RoleContentViewModel RoleContentViewModel => App.ServiceProvider.GetRequiredService<RoleContentViewModel>();
        public PermissionContentViewModel PermissionContentViewModel => App.ServiceProvider.GetRequiredService<PermissionContentViewModel>();
        public PermissionPopupViewModel PermissionPopupViewModel => App.ServiceProvider.GetRequiredService<PermissionPopupViewModel>();
        public PasswordPageViewModel PasswordPageViewModel => App.ServiceProvider.GetRequiredService<PasswordPageViewModel>();
        public PasswordContentViewModel PasswordContentViewModel => App.ServiceProvider.GetRequiredService<PasswordContentViewModel>();
        public PasswordPopupViewModel PasswordPopupViewModel => App.ServiceProvider.GetRequiredService<PasswordPopupViewModel>();
        public EbookPageViewModel EbookPageViewModel => App.ServiceProvider.GetRequiredService<EbookPageViewModel>();
        public EbookContentViewModel EbookContentViewModel => App.ServiceProvider.GetRequiredService<EbookContentViewModel>();
        public EbookPopupViewModel EbookPopupViewModel => App.ServiceProvider.GetRequiredService<EbookPopupViewModel>();
        public ActivityPageViewModel ActivityPageViewModel => App.ServiceProvider.GetRequiredService<ActivityPageViewModel>();
        public ActivityContentViewModel ActivityContentViewModel => App.ServiceProvider.GetRequiredService<ActivityContentViewModel>();
        public ActivityPopupViewModel ActivityPopupViewModel => App.ServiceProvider.GetRequiredService<ActivityPopupViewModel>();
        public ArticlePageViewModel ArticlePageViewModel => App.ServiceProvider.GetRequiredService<ArticlePageViewModel>();
        public ArticleContentViewModel ArticleContentViewModel => App.ServiceProvider.GetRequiredService<ArticleContentViewModel>();
        public ArticlePopupViewModel ArticlePopupViewModel => App.ServiceProvider.GetRequiredService<ArticlePopupViewModel>();
        public RepositoryPageViewModel RepositoryPageViewModel => App.ServiceProvider.GetRequiredService<RepositoryPageViewModel>();
        public RepositoryContentViewModel RepositoryContentViewModel => App.ServiceProvider.GetRequiredService<RepositoryContentViewModel>();
        public RepositoryPopupViewModel RepositoryPopupViewModel => App.ServiceProvider.GetRequiredService<RepositoryPopupViewModel>();
        public RepositoryOptionsContentViewModel RepositoryOptionsContentViewModel => App.ServiceProvider.GetRequiredService<RepositoryOptionsContentViewModel>();
        public FilterPopupViewModel FilterPopupViewModel => App.ServiceProvider.GetRequiredService<FilterPopupViewModel>();
        public TagsContentViewModel TagsContentViewModel => App.ServiceProvider.GetRequiredService<TagsContentViewModel>();
        public TagsPopupViewModel TagsPopupViewModel => App.ServiceProvider.GetRequiredService<TagsPopupViewModel>();
        public ToolsPageViewModel ToolsPageViewModel => App.ServiceProvider.GetRequiredService<ToolsPageViewModel>();
        public DatabaseContentViewModel DatabaseContentViewModel => App.ServiceProvider.GetRequiredService<DatabaseContentViewModel>();
        public EbookOptionsContentViewModel EbookOptionsContentViewModel => App.ServiceProvider.GetRequiredService<EbookOptionsContentViewModel>();
        public ArticleOptionsContentViewModel ArticleOptionsContentViewModel => App.ServiceProvider.GetRequiredService<ArticleOptionsContentViewModel>();
        public NoteContentViewModel NoteContentViewModel => App.ServiceProvider.GetRequiredService<NoteContentViewModel>();
        public NotePopupViewModel NotePopupViewModel => App.ServiceProvider.GetRequiredService<NotePopupViewModel>();
        public NotePageViewModel NotePageViewModel => App.ServiceProvider.GetRequiredService<NotePageViewModel>();
    }
}
