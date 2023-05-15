using System.Collections.Generic;

namespace ResourceManager.Configuration
{
    public static class FilterColumns
    {
        public static IEnumerable<string> User = new[] { "Name", "AccountName" };
        public static IEnumerable<string> Tag = new[] { "Name", "PasswordCount", "BookCount","ArticleCount","ActivityCount","RepositoryCount","NoteCount" };
        public static IEnumerable<string> Role = new[] { "Name", "Description" };
        public static IEnumerable<string> Permission = new[] { "Name", "Description" };
        public static IEnumerable<string> Book = new[] { "Title", "Author", "Publisher", "Abstraction", "TagString" };
        public static IEnumerable<string> Article = new[] { "Title", "Author", "TagString" };
        public static IEnumerable<string> Password = new[] { "Name", "UserName", "Url", "Description", "TagString" };
        public static IEnumerable<string> Repository = new[] { "Title", "FileType", "FilePath", "FileSize", "TagString" };
        public static IEnumerable<string> Activity = new[] { "Title", "Date", "StartTime", "EndTime", "Metric", "Quantity", "Output", "Note", "TagString" };
        public static IEnumerable<string> Note = new[] { "Title", "Date", "Notes", "TagString" };
    }
}
