namespace ResourceManager.Models
{
    public class RolePermission
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        // Relationships
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
