using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ResourceManager.Models
{
    public class Repository : BaseEntity, ITags, IEquatable<Repository>, IComparable<Repository>
    {
        public string Title { get; set; } = string.Empty;
        public string FileType { get; set; }= string.Empty;
        public string Filename { get; set; }= string.Empty;
        
        public int FileSize { get; set; }
        public string FilePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Filename))
                {
                    return Path.Combine(App.Settings.RepositorySettings.FolderPath, Filename);
                }
                return Filename;
            }
        }
        public IList<RepositoryTag> RepositoryTags { get; set; } = new List<RepositoryTag>();

        public string TagString => string.Join(", ", RepositoryTags.Select(x => x.Tag.Name));
        public override string GetCaption()
        {
            return string.Format("{0} with Title '{1}'", nameof(Repository), Title);
        }
        #region IEquatable<Repository>
        public bool Equals(Repository? other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Title.Equals(other.Title);
        }
        #endregion

        #region IComparable<Repository>
        public int CompareTo(Repository? other)
        {
            if (ReferenceEquals(null, other)) return -1;

            return Title.CompareTo(other.Title);
        }
        #endregion
    }
}
