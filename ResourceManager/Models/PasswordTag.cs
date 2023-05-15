namespace ResourceManager.Models
{
    public class PasswordTag
    {
        public int PasswordID { get; set; }
        public int TagID { get; set; }

        public Password Password { get; set; }
        public Tag Tag { get; set; }
    }
}
