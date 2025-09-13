namespace AuthService.Infrastructure.Data
{
    public class Databasesettings : IDatabasesettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}