namespace MovieManager.Services
{
    public interface IDatabaseSettings
    {
        string DatabaseCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}