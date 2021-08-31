using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Movies.API.Constants;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public class EntertainerRepository : IEntertainerRepository
    {
        private readonly IDynamoDBContext _db;

        public EntertainerRepository(IDynamoDBContext db)
        {
            _db = db;
        }
        public async Task Delete(Entertainer entertainer)
        {
            await _db.DeleteAsync(entertainer, DynamoConstants.DB_CONFIG);
        }

        public async Task<List<Entertainer>> Get()
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and begins_with(SK, :sk)";
            keyExpression.ExpressionAttributeValues.Add(":pk", SortKeyPrefixes.ENTERTAINER);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.ENTERTAINER);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<Entertainer>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results;
        }

        public async Task<Entertainer> Get(string id)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and SK = :sk";
            keyExpression.ExpressionAttributeValues.Add(":pk", SortKeyPrefixes.ENTERTAINER);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.ENTERTAINER + id);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<Entertainer>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results.FirstOrDefault();
        }

        public async Task Save(Entertainer entertainer)
        {
            await _db.SaveAsync(entertainer, DynamoConstants.DB_CONFIG);
        }
    }
}