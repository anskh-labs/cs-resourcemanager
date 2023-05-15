using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResourceManager.Models
{
    public class Article : BaseEntity, ITags, IEquatable<Article>, IComparable<Article>
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string FilePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Filename))
                {
                    return Path.Combine(App.Settings.ArticleSettings.FolderPath, Filename);
                }
                return Filename;
            }
        }
        public override string GetCaption()
        {
            return string.Format("{0} with title '{1}'", nameof(Article), Title);
        }
        public IList<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();

        public string TagString => string.Join(",", ArticleTags.Select(x => x.Tag.Name));
        #region IEquatable<Article>
        public bool Equals(Article? other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Title.Equals(other.Title);
        }
        #endregion

        #region IComparable<Article>
        public int CompareTo(Article? other)
        {
            if (ReferenceEquals(null, other)) return -1;

            return Title.CompareTo(other.Title);
        }
        #endregion
    }
}
