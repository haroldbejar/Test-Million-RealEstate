using AuthService.Domain.Interfaces;
using AuthService.Domain.Models;
using AuthService.Infrastructure.Data;
using MongoDB.Driver;

namespace AuthService.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        private readonly IMongoCollection<AppUser> _collection;
        public AppUserRepository(IDatabasesettings settings) : base(settings, "Users")
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<AppUser>("Users");
        }

        public async Task<AppUser> GetUserByUserName(string userName)
        {
            var filter = Builders<AppUser>.Filter.Eq("username", userName);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}