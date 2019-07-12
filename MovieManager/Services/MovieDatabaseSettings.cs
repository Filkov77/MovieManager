namespace MovieManager.Services
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}