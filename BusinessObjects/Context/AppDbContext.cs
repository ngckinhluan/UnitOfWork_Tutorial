using MongoDB.Driver;
using BusinessObjects.Entities;

namespace BusinessObjects.Context
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    }
}