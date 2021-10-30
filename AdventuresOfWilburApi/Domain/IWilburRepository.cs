using System.Collections.Generic;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Models;

namespace AdventuresOfWilburApi.Domain
{
    public interface IWilburRepository
    {
        Task<IEnumerable<WilburCard>> GetAll();
    }
}