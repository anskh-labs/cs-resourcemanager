using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceManager.Models
{
    public class Note : BaseEntity, ITags, IEquatable<Note>, IComparable<Note>
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User User { get; set; }
        public IList<NoteTag> NoteTags { get; set; } = new List<NoteTag>();

        public string TagString => string.Join(", ", NoteTags.Select(x => x.Tag.Name));

        public override string GetCaption()
        {
            return string.Format("{0} with Title {1}", nameof(Note), Title);
        }

        #region IEquatable<Note>
        public bool Equals(Note? other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Title.Equals(other.Title) && Date.Equals(other.Date);
        }
        #endregion

        #region IComparable<Note>
        public int CompareTo(Note? other)
        {
            if (ReferenceEquals(null, other)) return -1;
            int result = Date.CompareTo(other.Date);
            if (result == 0)
            {    
                return Title.CompareTo(other.Title);
            }
            return result;
        }
        #endregion
}
}
