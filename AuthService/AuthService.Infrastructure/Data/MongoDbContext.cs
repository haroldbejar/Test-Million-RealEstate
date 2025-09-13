
using AuthService.Domain.Models;
using MongoDB.Driver;
namespace AuthService.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IDatabasesettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<AppUser> Users =>
           _database.GetCollection<AppUser>("Users");
    }
}