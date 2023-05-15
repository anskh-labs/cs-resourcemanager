namespace ResourceManager.Models
{
    public class RepositoryTag
    {
        public int RepositoryID { get; set; }
        public int TagID { get; set; }

        public Repository Repository { get; set; }
        public Tag Tag { get; set; }
    }
}
