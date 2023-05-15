namespace ResourceManager.Services.Abstractions
{
    public interface IDataServiceManager
    {
        IUserDataService UserDataService { get; }
        IRoleDataService RoleDataService { get; }
        IPermissionDataService PermissionDataService { get; }
        IPasswordDataService PasswordDataService { get; }
        IBookDataService BookDataService { get; }
        IRepositoryDataService RepositoryDataService { get; }
        IArticleDataService ArticleDataService { get; }
        IActivityDataService ActivityDataService { get; }
        ITagDataService TagDataService { get; }
        INoteDataService NoteDataService { get; }
    }
}
