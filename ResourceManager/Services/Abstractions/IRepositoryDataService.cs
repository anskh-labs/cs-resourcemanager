using ResourceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IRepositoryDataService : IDataService<Repository>
    {
        Task<int> UpdateTags(Repository repo, IList<Tag> tags);
    }
}
