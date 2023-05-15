namespace ResourceManager.Models.Abstractions
{
    public interface IEntity
    {
        int ID { get; set; }
        string GetCaption();
    }
}
