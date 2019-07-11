namespace MovieManager.Services
{
    public class MovieDatabaseSettings : IMovieDatabaseSettings
    {
        public string MovieCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}