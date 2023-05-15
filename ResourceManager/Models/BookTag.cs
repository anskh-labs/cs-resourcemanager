namespace ResourceManager.Models
{
    public class BookTag
    {
        public int BookID { get; set; }
        public int TagID { get; set; }

        public Book Book { get; set; }
        public Tag Tag { get; set; }
    }
}
