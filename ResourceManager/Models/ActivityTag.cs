namespace ResourceManager.Models
{
    public class ActivityTag
    {
        public int ActivityID { get; set; }
        public int TagID { get; set; }

        public Activity Activity { get; set; }
        public Tag Tag { get; set; }
    }
}
