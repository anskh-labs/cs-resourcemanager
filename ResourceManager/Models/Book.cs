using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResourceManager.Models
{
    public class Book : BaseEntity, ITags, IEquatable<Book>, IComparable<Book>
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public string Abstraction { get; set; } = string.Empty;
        public IList<BookTag> BookTags { get; set; } = new List<BookTag>();

        public string TagString => string.Join(", ", BookTags.Select(x => x.Tag.Name));
        public string FilePath 
        {
            get
            {
                if (!string.IsNullOrEmpty(Filename))
                {
                    return Path.Combine(App.Settings.EbookSettings.FolderPath, Filename);
                }
                return Filename;
            }
        }
        public string CoverPath
        {
            get
            {
                if (!string.IsNullOrEmpty(Cover))
                {
                    return Path.Combine(App.Settings.EbookSettings.CoverPath, Cover);
                }
                return Cover;
            }
        }
        public override string GetCaption()
        {
            return string.Format("{0} with title '{1}'", nameof(Book), Title);
        }

        #region IEquatable<Book>
        public bool Equals(Book? other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Title.Equals(other.Title);
        }
        #endregion

        #region IComparable<Book>
        public int CompareTo(Book? other)
        {
            if (ReferenceEquals(null, other)) return -1;

            return Title.CompareTo(other.Title);
        }
        #endregion
    }
}