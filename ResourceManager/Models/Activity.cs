using ResourceManager.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceManager.Models
{
    public class Activity : BaseEntity, ITags, IEquatable<Activity>, IComparable<Activity>
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public string Metric { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Output { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User User { get; set; }
        public IList<ActivityTag> ActivityTags { get; set; } = new List<ActivityTag>();

        public string TagString => string.Join(", ", ActivityTags.Select(x => x.Tag.Name));

        public override string GetCaption()
        {
            return string.Format("{0} with Title {1}", nameof(Activity), Title);
        }

        #region IEquatable<Activity>
        public bool Equals(Activity? other)
        {
            if (ReferenceEquals(null, other)) return false;

            return Title.Equals(other.Title) && Date.Equals(other.Date);
        }
        #endregion

        #region IComparable<Activity>
        public int CompareTo(Activity? other)
        {
            if (ReferenceEquals(null, other)) return -1;
            int result = Date.CompareTo(other.Date);
            if (result == 0)
            {
                return Title.CompareTo(other.Title);
            }
            return result;
        }
        #endregion
    }
}
