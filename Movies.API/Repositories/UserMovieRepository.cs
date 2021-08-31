using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Movies.API.Constants;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public class UserMovieRepository : IUserMovieRepository
    {
        private readonly IDynamoDBContext _db;

        public UserMovieRepository(IDynamoDBContext db)
        {
            _db = db;
        }
        public async Task Delete(UserMovie userMovie)
        {
            await _db.DeleteAsync(userMovie, DynamoConstants.DB_CONFIG);
        }

        public async Task<List<UserMovie>> Get(string pk)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and begins_with(SK, :sk)";
            keyExpression.ExpressionAttributeValues.Add(":pk", pk);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.USER_MOVIE);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<UserMovie>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results;
        }

        public async Task<UserMovie> Get(string pk, string movieId)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and SK = :sk";
            keyExpression.ExpressionAttributeValues.Add(":pk", pk);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.USER_MOVIE + movieId);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<UserMovie>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results.FirstOrDefault();
        }

        public async Task Save(UserMovie userMovie)
        {
            await _db.SaveAsync(userMovie, DynamoConstants.DB_CONFIG);
        }
    }
}