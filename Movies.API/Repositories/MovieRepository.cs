using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Movies.API.Constants;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDynamoDBContext _db;

        public MovieRepository(IDynamoDBContext db)
        {
            _db = db;
        }
        public async Task Delete(Movie movie)
        {
            await _db.DeleteAsync(movie, DynamoConstants.DB_CONFIG);
        }

        public async Task<List<Movie>> Get()
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and begins_with(SK, :sk)";
            keyExpression.ExpressionAttributeValues.Add(":pk", SortKeyPrefixes.MOVIE);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.MOVIE);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<Movie>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results;
        }

        public async Task<Movie> Get(string id)
        {
            var queryOperation = new QueryOperationConfig();
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "PK = :pk and SK = :sk";
            keyExpression.ExpressionAttributeValues.Add(":pk", SortKeyPrefixes.MOVIE);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.MOVIE + id);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<Movie>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results.FirstOrDefault();
        }

        public async Task Save(Movie movie)
        {
            await _db.SaveAsync(movie, DynamoConstants.DB_CONFIG);
        }
    }
}