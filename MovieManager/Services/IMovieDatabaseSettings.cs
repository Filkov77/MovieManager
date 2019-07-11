namespace MovieManager.Services
{
    public interface IMovieDatabaseSettings
    {
        string MovieCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}