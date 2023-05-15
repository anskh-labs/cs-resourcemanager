using ResourceManager.Models.Abstractions;
using System;

namespace ResourceManager.Models
{
    public class BaseEntity : IEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual string GetCaption()
        {
            return string.Format("{0} with ID {1}", GetType().Name, ID);
        }
    }
}