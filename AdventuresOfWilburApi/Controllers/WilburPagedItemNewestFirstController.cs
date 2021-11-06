using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using AdventuresOfWilburApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdventuresOfWilburApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WilburPagedItemNewestFirstController
    {
        private readonly IWilburRepository _wilburRepository;

        public WilburPagedItemNewestFirstController(IWilburRepository wilburRepository)
        {
            _wilburRepository = wilburRepository;
        }

        [EnableCors("_MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<IEnumerable<WilburCard>> Get([FromQuery] long page, [FromQuery] long limit)
        {
            Console.WriteLine($"page {page}");
            Console.WriteLine($"limit {limit}");
            var pxl = page * limit;
            page = (await _wilburRepository.GetMostRecentIndex()) - (pxl);
            
            Console.WriteLine($"pxl {pxl}");
            Console.WriteLine($"actual {page}");
            
            var card = await _wilburRepository.GetItemsForIdAndLimitNewestFirst(page, limit);

            Console.WriteLine(JsonSerializer.Serialize(card.Select(x => x.Id)));
            
            return card;
        }
    }
}