using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface INoteDataService:IDataService<Note>
    {
        Task<int> UpdateTags(Note note, IList<Tag> tags);
    }
}