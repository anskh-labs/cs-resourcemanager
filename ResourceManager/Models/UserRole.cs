namespace ResourceManager.Models
{
    public class UserRole
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        //Relationships
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
