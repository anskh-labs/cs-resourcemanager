using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IArticleDataService:IDataService<Article>
    {
        Task<int> UpdateTags(Article article, IList<Tag> tags);
    }
}