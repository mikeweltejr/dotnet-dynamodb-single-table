using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Movies.API.Constants;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _db;

        public UserRepository(IDynamoDBContext db)
        {
            _db = db;
        }
        public async Task Delete(User user)
        {
            await _db.DeleteAsync(user, DynamoConstants.DB_CONFIG);
        }

        public async Task<List<User>> Get()
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and begins_with(SK, :sk)";
            keyExpression.ExpressionAttributeValues.Add(":pk", SortKeyPrefixes.USER);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.USER);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<User>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results;
        }

        public async Task<User> Get(string id)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and SK = :sk";
            keyExpression.ExpressionAttributeValues.Add(":pk", SortKeyPrefixes.USER);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.USER + id);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<User>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results.FirstOrDefault();
        }

        public async Task Save(User user)
        {
            await _db.SaveAsync(user, DynamoConstants.DB_CONFIG);
        }
    }
}