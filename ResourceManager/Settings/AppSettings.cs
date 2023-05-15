namespace ResourceManager.Settings
{
    public class AppSettings
    {
        public const string SectionName = "AppSettings";
        public string AppName { get; set; }
        public string AppVersion { get; set; } 
        public string AppLanguage { get; set; }
        public EbookSettings EbookSettings { get; set; }
        public ArticleSettings ArticleSettings { get; set; }
        public ActivitySettings ActivitySettings { get; set; }
        public RepositorySettings RepositorySettings { get; set; }
    }
    public class EbookSettings
    {
        public const string SectionName = "EbookSettings";
        public string FolderPath { get; set; }
        public string SupportExtensions { get; set; }
        public string CoverPath { get; set; }
        public string CoverFileExtensions { get; set; }
        public bool DeleteSourceFile { get; set; }
    }
    public class ArticleSettings
    {
        public const string SectionName = "ArticleSettings";
        public string FolderPath { get; set; }
        public string SupportExtensions { get; set; }
        public bool DeleteSourceFile { get; set; }
    }
    public class ActivitySettings
    {
        public const string SectionName = "ActivitySettings";

    }
    public class RepositorySettings
    {
        public const string SectionName = "RepositorySettings";
        public string FolderPath { get; set; }
        public string SupportExtensions { get; set; }
        public bool DeleteSourceFile { get; set; }
    }
}
