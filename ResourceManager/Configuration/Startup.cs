using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Extensions;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using ResourceManager.Services;
using ResourceManager.Settings;
using ResourceManager.ViewModels;
using ResourceManager.Views;
using NetCore.DatabaseToolkit.SQLite;

namespace ResourceManager.Configuration
{
    public static class Startup
    {
        public static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.SetBasePath(context.HostingEnvironment.ContentRootPath);
            builder.AddJsonFile(Constants.JsonFileName, optional: false, reloadOnChange: true);
            builder.AddEnvironmentVariables();
        }

        public static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
        {
            builder.AddConfiguration(context.Configuration.GetSection(LogSettings.SectionName));
            builder.AddDebug();
        }

        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            //register core mvvm
            services.AddMvvm();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            //Can access to generic IConfiguration
            services.AddSingleton(configuration);

            //Add custom Config object so it can be injected as IOptions<AppSettings>
            services.Configure<AppSettings>(configuration.GetSection(AppSettings.SectionName));
            services.Configure<SystemIntegrationSettings>(configuration.GetSection(SystemIntegrationSettings.SectionName));
            services.Configure<DBSettings>(configuration.GetSection(DBSettings.ConnectionStrings));

            //register services
            services.AddScoped<IDesignTimeDbContextFactory<ResourceManagerDBContext>, ResourceManagerDBContextFactory>();
            services.AddScoped<IDataServiceManager, DataServiceManager>();
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IRoleDataService, RoleDataService>();
            services.AddScoped<IPasswordDataService, PasswordDataService>();
            services.AddScoped<IPermissionDataService, PermissionDataService>();
            services.AddScoped<IBookDataService, BookDataService>();
            services.AddScoped<IRepositoryDataService, RepositoryDataService>();
            services.AddScoped<IActivityDataService, ActivityDataService>();
            services.AddScoped<IArticleDataService, ArticleDataService>();
            services.AddScoped<ITagDataService, TagDataService>();
            services.AddScoped<INoteDataService, NoteDataService>();
            services.AddScoped<ISQLiteToolkit, SQLiteToolkit>();

            //register mainvm
            services.AddSingleton<IPopupable, MainViewModel>();
            services.AddSingleton<ViewModelLocator>();

            //register relation viewmodel-view
            services.AddMvvmSingleton<MainViewModel, MainView>();
            services.AddMvvmTransient<LoginPageViewModel, LoginPageView>();
            services.AddMvvmTransient<MainPageViewModel, MainPageView>();
            services.AddMvvmTransient<UserPageViewModel, UserPageView>();
            services.AddMvvmTransient<PasswordPageViewModel, PasswordPageView>();
            services.AddMvvmTransient<EbookPageViewModel, EbookPageView>();
            services.AddMvvmTransient<EbookContentViewModel, EbookContentView>();
            services.AddMvvmTransient<EbookPopupViewModel, EbookPopupView>();
            services.AddMvvmTransient<RepositoryPageViewModel, RepositoryPageView>();
            services.AddMvvmTransient<RepositoryContentViewModel, RepositoryContentView>();
            services.AddMvvmTransient<RepositoryPopupViewModel, RepositoryPopupView>();
            services.AddMvvmTransient<ArticlePageViewModel, ArticlePageView>();
            services.AddMvvmTransient<ArticleContentViewModel, ArticleContentView>();
            services.AddMvvmTransient<ArticlePopupViewModel, ArticlePopupView>();
            services.AddMvvmTransient<ActivityPageViewModel, ActivityPageView>();
            services.AddMvvmTransient<ActivityContentViewModel, ActivityContentView>();
            services.AddMvvmTransient<ActivityPopupViewModel, ActivityPopupView>();
            services.AddMvvmTransient<NotePageViewModel, NotePageView>();
            services.AddMvvmTransient<NoteContentViewModel, NoteContentView>();
            services.AddMvvmTransient<NotePopupViewModel, NotePopupView>();
            services.AddMvvmTransient<UserContentViewModel, UserContentView>();
            services.AddMvvmTransient<RoleContentViewModel, RoleContentView>();
            services.AddMvvmTransient<PermissionContentViewModel, PermissionContentView>();
            services.AddMvvmTransient<UserPopupViewModel, UserPopupView>();
            services.AddMvvmTransient<FilterPopupViewModel, FilterPopupView>();
            services.AddMvvmTransient<UserRolesPopupViewModel, UserRolesPopupView>();
            services.AddMvvmTransient<RolePopupViewModel, RolePopupView>();
            services.AddMvvmTransient<RolePermissionsPopupViewModel, RolePermissionsPopupView>();
            services.AddMvvmTransient<PermissionPopupViewModel, PermissionPopupView>();
            services.AddMvvmTransient<PasswordContentViewModel, PasswordContentView>();
            services.AddMvvmTransient<PasswordPopupViewModel, PasswordPopupView>();
            services.AddMvvmTransient<TagsContentViewModel, TagsContentView>();
            services.AddMvvmTransient<TagsPopupViewModel, TagsPopupView>();
            services.AddMvvmTransient<ToolsPageViewModel, ToolsPageView>();
            services.AddMvvmTransient<DatabaseContentViewModel, DatabaseContentView>();
            services.AddMvvmTransient<EbookOptionsContentViewModel, EbookOptionsContentView>();
            services.AddMvvmTransient<ArticleOptionsContentViewModel, ArticleOptionsContentView>();
            services.AddMvvmTransient<RepositoryOptionsContentViewModel, RepositoryOptionsContentView>();
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
            => ConfigureServices(context.Configuration, services);
    }
}