using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Domain;
using AdventuresOfWilburApi.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;

namespace AdventuresOfWilburApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WilburFeedController
    {
        private readonly IWilburRepository _wilburRepository;

        public WilburFeedController(IWilburRepository wilburRepository)
        {
            _wilburRepository = wilburRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WilburCard>> Get()
        {
            return await _wilburRepository.GetAll();
        }
    }
}