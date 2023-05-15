using System.Windows.Media;

namespace ResourceManager.Configuration
{
    public static class Constants
    {
        public const string JsonFileName = "appsettings.json";
        public const string DbFileName = "ResourceManager.db";

        public static readonly Brush UserPageColor = new SolidColorBrush(Color.FromRgb(0, 51, 238));
        public static readonly Brush PasswordPageColor = new SolidColorBrush(Color.FromRgb(4, 149, 84));
        public static readonly Brush RepositoryPageColor = new SolidColorBrush(Color.FromRgb(247, 99, 12));
        public static readonly Brush EbookPageColor = new SolidColorBrush(Color.FromRgb(232, 17, 25));
        public static readonly Brush ArticlePageColor = new SolidColorBrush(Color.FromRgb(191, 0, 119));
        public static readonly Brush ActivityPageColor = new SolidColorBrush(Color.FromRgb(77, 74, 142));
        public static readonly Brush NotePageColor = new SolidColorBrush(Color.FromRgb(34, 119, 170));
        public static readonly Brush ToolsPageColor = new SolidColorBrush(Color.FromRgb(74, 84, 89));
        public static readonly Brush DefaultPageColor = new SolidColorBrush(Color.FromRgb(80, 80, 80));

        public const string VIEW_USER_PERMISSION = "v_user";
        public const string VIEW_PASSWORD_PERMISSION = "v_password";
        public const string VIEW_REPOSITORY_PERMISSION = "v_repository";
        public const string VIEW_EBOOK_PERMISSION = "v_ebook";
        public const string VIEW_ARTICLE_PERMISSION = "v_article";
        public const string VIEW_ACTIVITY_PERMISSION = "v_activity";
        public const string VIEW_NOTE_PERMISSION = "v_note";
        public const string VIEW_TOOLS_PERMISSION = "v_tools";

        public const string ACTION_ADD_USER = "a_add_user";
        public const string ACTION_EDIT_USER = "a_edit_user";
        public const string ACTION_DEL_USER = "a_del_user";

        public const string ACTION_ADD_ROLE = "a_add_role";
        public const string ACTION_EDIT_ROLE = "a_edit_role";
        public const string ACTION_DEL_ROLE = "a_del_role";

        public const string ACTION_ADD_PERMISSION = "a_add_perm";
        public const string ACTION_EDIT_PERMISSION = "a_edit_perm";
        public const string ACTION_DEL_PERMISSION = "a_del_perm";

        public const string ACTION_ADD_PASSWORD = "a_add_password";
        public const string ACTION_EDIT_PASSWORD = "a_edit_password";
        public const string ACTION_DEL_PASSWORD = "a_del_password";

        public const string ACTION_ADD_TAG = "a_add_tag";
        public const string ACTION_EDIT_TAG = "a_edit_tag";
        public const string ACTION_DEL_TAG = "a_del_tag";

        public const string ACTION_ADD_REPOSITORY = "a_add_repository";
        public const string ACTION_EDIT_REPOSITORY = "a_edit_repository";
        public const string ACTION_DEL_REPOSITORY = "a_del_repository";

        public const string ACTION_ADD_EBOOK = "a_add_ebook";
        public const string ACTION_EDIT_EBOOK = "a_edit_ebook";
        public const string ACTION_DEL_EBOOK = "a_del_ebook";

        public const string ACTION_ADD_ARTICLE = "a_add_article";
        public const string ACTION_EDIT_ARTICLE = "a_edit_article";
        public const string ACTION_DEL_ARTICLE = "a_del_article";

        public const string ACTION_ADD_ACTIVITY = "a_add_activity";
        public const string ACTION_EDIT_ACTIVITY = "a_edit_activity";
        public const string ACTION_DEL_ACTIVITY = "a_del_activity";

        public const string ACTION_ADD_NOTE = "a_add_note";
        public const string ACTION_EDIT_NOTE = "a_edit_note";
        public const string ACTION_DEL_NOTE = "a_del_note";
    }
}
