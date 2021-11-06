using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdventuresOfWilburApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetMostRecentIndexController
    {
        private readonly IWilburRepository _wilburRepository;

        public GetMostRecentIndexController(IWilburRepository wilburRepository)
        {
            _wilburRepository = wilburRepository;
        }

        [EnableCors("_MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<long> Get([FromQuery] int page, [FromQuery] int limit)
        {
            var maxIndex = await _wilburRepository.GetMostRecentIndex();
            return maxIndex;
        }
    }
}