using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventuresOfWilburApi.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AdventuresOfWilburApi.Domain
{
    public class WilburRepository : IWilburRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public WilburRepository(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        public async Task<IEnumerable<WilburCard>> GetAll()
        {
            Dictionary<string, AttributeValue> exclusiveStartKey = null;

            var scanResults = new List<Dictionary<string, AttributeValue>>();
            
            do
            {
                var scanResponse = await _dynamoDb.ScanAsync(new ScanRequest
                {
                    TableName = "AdventuresOfWilburImageTable",
                    Select = "ALL_ATTRIBUTES",
                    ExclusiveStartKey = exclusiveStartKey
                });

                scanResults.AddRange(scanResponse.Items.ToList());
                exclusiveStartKey = scanResponse.LastEvaluatedKey;
                Console.WriteLine(scanResponse.Items.Count);
            } while (exclusiveStartKey.ContainsKey("ImageId"));

            return scanResults.Select(item => new WilburCard(long.Parse(item["ImageId"].N), item["Title"].S, item["Description"].S, item["ImageKey"].S)).OrderByDescending(x => x.Id).ToList();
        }
    }
}