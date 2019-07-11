using MovieManager.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using MongoDB.Bson;

namespace MovieManager.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<Movie> _Movies;

        public MovieService(IMovieDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Movies = database.GetCollection<Movie>(settings.MovieCollectionName);

            //var con = new MongoUrl("mongodb+srv://service:rSSCDZTIZzwPRsnEHGvY@cluster0-iv5oy.mongodb.net/movie?retryWrites=true");
            //var _client = new MongoClient(MongoClientSettings.FromUrl(con));
            //var _database = _client.GetDatabase(MongoDatabaseName);
            //var collectionNames = _database.ListCollections();
        }

        public async Task<List<Movie>> GetAsync(string searchString, CancellationToken cancellationToken = default)
        {
            // TODO if more complex filter needed, create a filter class
            FilterDefinition<Movie> filter = string.IsNullOrEmpty(searchString) ? Builders<Movie>.Filter.Empty : Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(searchString));

            var allMoves = await _Movies.FindAsync(filter, null, cancellationToken);

            return await (allMoves).ToListAsync();
            // string.IsNullOrEmpty(searchString) ? true : generateTitleFilter(movie, searchString)
        }
        //TODO if extended, generate a FilterService for it
        private bool generateTitleFilter(Movie movie, string searchString) => string.IsNullOrEmpty(searchString) ? true : movie.Title.Contains(searchString);

        public async Task<Movie> GetDetailsAsync(string id, CancellationToken cancellationToken = default) =>
            await (await _Movies.FindAsync(movie => movie.Id == id)).FirstOrDefaultAsync();

        public async Task<Movie> CreateAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            await _Movies.InsertOneAsync(movie, null, cancellationToken);
            return movie;
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, Movie movieIn, CancellationToken cancellationToken = default) =>
            await _Movies.ReplaceOneAsync(movie => movie.Id == id, movieIn, null, cancellationToken);

        public async Task<DeleteResult> RemoveAsync(Movie movieIn, CancellationToken cancellationToken = default) =>
            await _Movies.DeleteOneAsync(movie => movie.Id == movieIn.Id, null, cancellationToken);

        public async Task<DeleteResult> RemoveAsync(string id, CancellationToken cancellationToken = default) =>
            await _Movies.DeleteOneAsync(movie => movie.Id == id, cancellationToken);
    }
}