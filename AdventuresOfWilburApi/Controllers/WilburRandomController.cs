using System;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using AdventuresOfWilburApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AdventuresOfWilburApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WilburRandomController
    {
        private readonly IWilburRepository _wilburRepository;

        public WilburRandomController(IWilburRepository wilburRepository)
        {
            _wilburRepository = wilburRepository;
        }

        [EnableCors("_MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<WilburCard> Get()
        {
            var max = await _wilburRepository.GetMostRecentIndex();
            var rand = new Random();

            if (max > int.MaxValue)
                max = int.MaxValue;

            var selectedItem = rand.Next(1, Convert.ToInt32(max));

            var randomWilburCard = await _wilburRepository.GetById(selectedItem);

            while (randomWilburCard == null)
                randomWilburCard = await _wilburRepository.GetById(selectedItem);

            return randomWilburCard;
        }
    }
}