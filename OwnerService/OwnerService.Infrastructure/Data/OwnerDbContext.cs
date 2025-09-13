using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OwnerService.Domain.Entities;

namespace OwnerService.Infrastructure.Data
{
    public class OwnerDbContext
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase(configuration.GetConnectionString("DatabaseName"));
            _owners = database.GetCollection<Owner>("Owners");
        }

        public IMongoCollection<Owner> Owners => _owners;
    }
}
