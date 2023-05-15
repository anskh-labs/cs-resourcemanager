using ResourceManager.Models.Abstractions;
using System.Collections.Generic;

namespace ResourceManager.Models
{
    public class Permission : BaseEntity, IItem
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public virtual IList<RolePermission> RolePermissions { get; set; }  = new List<RolePermission>();
        public override string GetCaption()
        {
            return string.Format("{0} with name '{1}'",nameof(Permission),  Name);
        }
    }
}
