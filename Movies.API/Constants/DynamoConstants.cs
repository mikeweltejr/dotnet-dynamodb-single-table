using Amazon.DynamoDBv2.DataModel;

namespace Movies.API.Constants
{
    public static class DynamoConstants
    {
        public static DynamoDBOperationConfig DB_CONFIG = new DynamoDBOperationConfig { OverrideTableName="MovieService"};
    }
}