using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IAmazonDynamoDB _dynamoDb;

        public WilburFeedController(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        [HttpGet]
        public async Task<IEnumerable<WilburCard>> Get()
        {
            // await _dynamoDb.GetItemAsync(new GetItemRequest
            // {
            //     TableName = "AdventuresOfWilburImageTable",
            //     Key = new Dictionary<string, AttributeValue>
            //     {
            //         {"ImageId", new AttributeValue{ N = "19" }}
            //     }
            // });

            return new List<WilburCard>
            {
                new WilburCard
                {
                    Id = 19,
                    Title = "Wilbur's Wilderness Weekend - Campfire",
                    Body = "Uh oh, that looks like fire, Wilbur, Wilbur! Are you ok? Fire is dangerous Wilbur! You will singe your fur. Come away from the fire Wilbur. How did you even manage to light the campfire? Phew, Wilbur says he's fine, he just wanted to get the whole wilderness experience by going wild camping, and in true camping style, the rain is pouring. Wilbur does not like getting wet, so he has set up a nice tent for himself. Consequently he is keeping his distance from the fire - at the moment at least. Thank goodness for that!",
                    WilburImage = "https://adventures-of-wilbur-images.s3.eu-west-2.amazonaws.com/Wilbur_2020_8_15_17_56_58_109.jpg"
                }
            };
        }
    }
}