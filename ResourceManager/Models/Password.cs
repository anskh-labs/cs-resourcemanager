using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceManager.Models
{
    public class Password : BaseEntity, IItem, ITags, IEquatable<Password>, IComparable<Password>
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Pass { get; set; }= string.Empty;
        public string Url { get; set; }= string.Empty;
        public string Description { get; set; }= string.Empty;

        public int UserID { get; set; }
        public User User { get; set; }
        public IList<PasswordTag> PasswordTags { get; set; } = new List<PasswordTag>();

        public string TagString => string.Join(", ", PasswordTags.Select(x=>x.Tag.Name));
        public override string GetCaption()
        {
            return string.Format("{0} with name '{1}'", nameof(Password), Name);
        }

        #region IEquatable<Password>
        public bool Equals(Password? other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Name.Equals(other.Name);
        }
        #endregion

        #region IComparable<Password>
        public int CompareTo(Password? other)
        {
            if (ReferenceEquals(null, other)) return -1;

            return Name.CompareTo(other.Name);
        }
        #endregion
    }
}