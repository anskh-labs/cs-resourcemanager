using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace ResourceManager.Models
{
    public class User : BaseEntity, IItem
    {
        public string Name { get; set; } = string.Empty;
        public string AccountName { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual IList<Password> Passwords { get; set; } = new List<Password>();
        public virtual IList<Activity> Activities { get; set; } = new List<Activity>();
        public virtual IList<Note> Notes { get; set; } = new List<Note>();
        public override string GetCaption()
        {
            return string.Format("{0} with Name '{1}'", nameof(User), Name);
        }

    }
}
