using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PropertyService.Domain.Entities;

namespace PropertyService.Infrastructure.Data
{
    public class PropertyDbContext
    {
        private readonly IMongoCollection<Property> _properties;

        public PropertyDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(configuration.GetConnectionString("DatabaseName"));
            _properties = database.GetCollection<Property>("Properties");
        }

        public IMongoCollection<Property> Properties => _properties;
    }
}