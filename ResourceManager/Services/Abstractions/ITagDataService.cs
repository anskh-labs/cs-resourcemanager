using ResourceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface ITagDataService : IDataService<Tag>
    {
        Task<IList<Tag>> GetTagObjectForPasswordID(int id);
        Task<IList<Tag>> GetTagObjectForBookID(int id);
        Task<IList<Tag>> GetTagObjectForArticleID(int id);
        Task<IList<Tag>> GetTagObjectForRepositoryID(int id);
        Task<IList<Tag>> GetTagObjectForActivityID(int id);
        Task<IList<Tag>> GetTagObjectForNoteID(int id);
    }
}
