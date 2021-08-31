using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Movies.API.Constants;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public class EntertainerMovieRepository : IEntertainerMovieRepository
    {
        private readonly IDynamoDBContext _db;

        public EntertainerMovieRepository(IDynamoDBContext db)
        {
            _db = db;
        }
        public async Task<List<MovieEntertainer>> Get(string entertainerId)
        {
            var queryOperation = new QueryOperationConfig();
            queryOperation.IndexName = "GSI_1_INDEX";
            var keyExpression = new Expression();
            keyExpression.ExpressionStatement = "GSI_1 = :pk and begins_with(SK, :sk)";
            keyExpression.ExpressionAttributeValues.Add(":pk", entertainerId);
            keyExpression.ExpressionAttributeValues.Add(":sk", SortKeyPrefixes.MOVIE_ENT);
            queryOperation.KeyExpression = keyExpression;
            var queryResult = _db.FromQueryAsync<MovieEntertainer>(queryOperation, DynamoConstants.DB_CONFIG);
            var results = await queryResult.GetRemainingAsync();

            return results;
        }
    }
}