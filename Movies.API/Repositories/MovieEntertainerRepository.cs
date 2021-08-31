using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Movies.API.Constants;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public class MovieEntertainerRepository : IMovieEntertainerRepository
    {
        private readonly IDynamoDBContext _db;

        public MovieEntertainerRepository(IDynamoDBContext db)
        {
            _db = db;
        }
        public async Task Delete(MovieEntertainer movieEntertainer)
        {
            await _db.DeleteAsync(movieEntertainer, DynamoConstants.DB_CONFIG);
        }

        public async Task<List<MovieEntertainer>> Get(string pk)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and begins_with(SK, :sk)";
            keyExpression.ExpressionAttributeValues.Add(":pk", pk);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.MOVIE_ENT);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<MovieEntertainer>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results;
        }

        public async Task<MovieEntertainer> Get(string pk, string entertainerId)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and SK = :sk";
            keyExpression.ExpressionAttributeValues.Add(":pk", pk);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.MOVIE_ENT + entertainerId);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<MovieEntertainer>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results.FirstOrDefault();
        }

        public async Task Save(MovieEntertainer movieEntertainer)
        {
            await _db.SaveAsync(movieEntertainer, DynamoConstants.DB_CONFIG);
        }
    }
}