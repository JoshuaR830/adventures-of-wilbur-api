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

        public async Task<WilburCard> GetMostRecent()
        {
            // Try to get a more recent one - maybe dynamo hasn't updated it's thing yet
            return 
                await GetById(await GetMostRecentIndex() + 1) ?? 
                await GetById(await GetMostRecentIndex());
        }

        public async Task<long> GetMostRecentIndex()
        {
            var description = await _dynamoDb.DescribeTableAsync(new DescribeTableRequest
            {
                TableName = "AdventuresOfWilburImageTable"
            });
             
            return description.Table.ItemCount;
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
            } while (exclusiveStartKey.ContainsKey("ImageId"));

            return scanResults.Select(item => new WilburCard(long.Parse(item["ImageId"].N), item["Title"].S, item["Description"].S, item["ImageKey"].S)).OrderByDescending(x => x.Id).ToList();
        }

        public async Task<WilburCard> GetById(long id)
        {
            var itemResult = await _dynamoDb.GetItemAsync(new GetItemRequest
            {
                Key = new Dictionary<string, AttributeValue>
                {
                    {"ImageId", new AttributeValue { N = id.ToString()}}
                },
                TableName = "AdventuresOfWilburImageTable"
            });

            if (!itemResult.IsItemSet)
                return null;

            var item = itemResult.Item;

            return new WilburCard(long.Parse(item["ImageId"].N), item["Title"].S, item["Description"].S, item["ImageKey"].S);
        }

        public async Task<IEnumerable<WilburCard>> GetItemsForIdAndLimitNewestFirst(long itemId, long limit)
        {
            // This should get the very latest Wilbur picture - even if dynamo hasn't updated it's table count
            // By adding one each time it should ensure that if dynamo doesn't have the latest, that an image later isn't skipped because 1 will always be added
            if (await GetById(await GetMostRecentIndex() + 1) != null)
                itemId++;
            
            var keys = new List<Dictionary<string, AttributeValue>>();

            if (itemId - limit < 1)
                limit = itemId - 1L;

            for (var i = itemId; i > (itemId - limit); i--)
            {
                keys.Add(new() {{"ImageId", new AttributeValue {N = i.ToString()}}});
            }

            var items = await _dynamoDb.BatchGetItemAsync(new BatchGetItemRequest
            {
                RequestItems = new Dictionary<string, KeysAndAttributes>
                {
                    {
                        "AdventuresOfWilburImageTable", new KeysAndAttributes
                        {
                            Keys = keys
                        }
                    }
                }
            });
            
            return items.Responses["AdventuresOfWilburImageTable"].Select(item => new WilburCard(long.Parse(item["ImageId"].N), item["Title"].S, item["Description"].S, item["ImageKey"].S)).OrderByDescending(x => x.Id).ToList();
        }
    }
}