using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using ResourceManager.Models;
using ResourceManager.Services.Abstractions;
using System;

namespace ResourceManager.Services
{
    public class DataServiceManager : IDataServiceManager
    {
        public IUserDataService UserDataService => App.ServiceProvider.GetRequiredService<IUserDataService>();
        public IRoleDataService RoleDataService => App.ServiceProvider.GetRequiredService<IRoleDataService>();
        public IPermissionDataService PermissionDataService => App.ServiceProvider.GetRequiredService<IPermissionDataService>();
        public IPasswordDataService PasswordDataService => App.ServiceProvider.GetRequiredService<IPasswordDataService>();
        public IBookDataService BookDataService => App.ServiceProvider.GetRequiredService<IBookDataService>();
        public IRepositoryDataService RepositoryDataService => App.ServiceProvider.GetRequiredService<IRepositoryDataService>();
        public IArticleDataService ArticleDataService => App.ServiceProvider.GetRequiredService<IArticleDataService>();
        public IActivityDataService ActivityDataService => App.ServiceProvider.GetRequiredService<IActivityDataService>();
        public ITagDataService TagDataService => App.ServiceProvider.GetRequiredService<ITagDataService>();
        public INoteDataService NoteDataService => App.ServiceProvider.GetRequiredService<INoteDataService>();
    }
}
