namespace ResourceManager.Models
{
    public class ArticleTag
    {
        public int ArticleID { get; set; }
        public int TagID { get; set; }

        public Article Article { get; set; }
        public Tag Tag { get; set; }
    }
}
