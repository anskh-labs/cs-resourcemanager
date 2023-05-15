using ResourceManager.Models.Abstractions;
using System.Collections.Generic;

namespace ResourceManager.Models
{
    public class Tag : BaseEntity, IItem
    {
        public string Name { get; set; } = string.Empty;
        public virtual IList<PasswordTag> PasswordTags { get; set; } = new List<PasswordTag>();
        public virtual IList<BookTag> BookTags { get; set; } = new List<BookTag>();
        public virtual IList<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
        public virtual IList<ActivityTag> ActivityTags { get; set; } = new List<ActivityTag>();
        public virtual IList<RepositoryTag> RepositoryTags { get; set; } = new List<RepositoryTag>();
        public virtual IList<NoteTag> NoteTags { get; set; } = new List<NoteTag>();

        public int PasswordCount { get { return PasswordTags.Count; } }
        public int BookCount { get { return BookTags.Count; } }
        public int ArticleCount { get { return ArticleTags.Count; } }
        public int ActivityCount { get { return ActivityTags.Count; } }
        public int RepositoryCount { get { return RepositoryTags.Count; } }
        public int NoteCount { get { return NoteTags.Count; } }

        public override string GetCaption()
        {
            return string.Format("{0} with Name '{1}'", nameof(Tag), Name);
        }
    }
}
