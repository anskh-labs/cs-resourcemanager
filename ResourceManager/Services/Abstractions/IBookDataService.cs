using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IBookDataService:IDataService<Book>
    {
        Task<int> UpdateTags(Book book, IList<Tag> tags);
    }
}