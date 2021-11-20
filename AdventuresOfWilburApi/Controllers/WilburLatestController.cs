using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using AdventuresOfWilburApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdventuresOfWilburApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WilburLatestController
    {
        private readonly IWilburRepository _wilburRepository;

        public WilburLatestController(IWilburRepository wilburRepository)
        {
            _wilburRepository = wilburRepository;
        }

        [EnableCors("_MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<WilburCard> Get()
        {
            return await _wilburRepository.GetMostRecent();
        }
    }
}