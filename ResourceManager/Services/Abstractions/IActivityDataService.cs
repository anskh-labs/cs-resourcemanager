using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IActivityDataService:IDataService<Activity>
    {
        Task<int> UpdateTags(Activity activity, IList<Tag> tags);
    }
}