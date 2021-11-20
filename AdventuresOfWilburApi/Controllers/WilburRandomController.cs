using System;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using AdventuresOfWilburApi.Models;
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

        [HttpGet]
        public async Task<WilburCard> Get()
        {
            await _wilburRepository.GetMostRecentIndex();
            var rand = new Random();
            var selectedItem = rand.Next();

            var randomWilburCard = await _wilburRepository.GetById(selectedItem);

            while (randomWilburCard == null)
                randomWilburCard = await _wilburRepository.GetById(selectedItem);

            return randomWilburCard;
        }
    }
}