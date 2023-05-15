using ResourceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManager.Services.Abstractions
{
    public interface IPasswordDataService : IDataService<Password>
    {
        Task<int> UpdateTags(Password pass, IList<Tag> tags);
    }
}
