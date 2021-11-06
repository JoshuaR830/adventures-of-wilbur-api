using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Models;

namespace AdventuresOfWilburApi.Domain
{
    public interface IWilburRepository
    {
        Task<IEnumerable<WilburCard>> GetMostRecent();
        Task<long> GetMostRecentIndex();
        Task<IEnumerable<WilburCard>> GetAll();
        Task<WilburCard> GetById(long id);
        Task<IEnumerable<WilburCard>> GetItemsForIdAndLimitNewestFirst(long itemId, long limit);
    }
}