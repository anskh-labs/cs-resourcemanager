using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace ResourceManager.Models
{
    public class Role : BaseEntity, IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; } = new List<UserRole>(); 
        public virtual IList<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public override string GetCaption()
        {
            return string.Format("{0} with Name '{1}'", nameof(Role), Name);
        }

    }
}
