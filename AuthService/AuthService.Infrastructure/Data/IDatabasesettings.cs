namespace AuthService.Infrastructure.Data
{
    public interface IDatabasesettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}