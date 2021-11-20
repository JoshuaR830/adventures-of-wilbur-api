using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using AdventuresOfWilburApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdventuresOfWilburApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WilburStoryController
    {
        private readonly IWilburRepository _wilburRepository;

        public WilburStoryController(IWilburRepository wilburRepository)
        {
            _wilburRepository = wilburRepository;
        }

        [EnableCors("_MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<WilburCard> Get([FromQuery] int currentPage, [FromQuery] bool isNext)
        {
            // If null cope by returning the same item again
            if (isNext)
                return await _wilburRepository.GetById(currentPage + 1) ?? await _wilburRepository.GetById(currentPage);
            
            return await _wilburRepository.GetById(currentPage - 1) ?? await _wilburRepository.GetById(currentPage);
        }
    }
}