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
    public class DbService<T> : IDbService<T> where T : IIdentifiable
    {
        private readonly IMongoCollection<T> _movies;

        public DbService(IMovieDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _movies = database.GetCollection<T>(settings.MovieCollectionName);
        }

        public async Task<List<T>> GetAsync(string searchString, string propertyName, CancellationToken cancellationToken = default)
        {
            // TODO move filter to appropriate class
            FilterDefinition<T> filter = string.IsNullOrEmpty(searchString) ? Builders<T>.Filter.Empty : Builders<T>.Filter.Regex(propertyName, new BsonRegularExpression(searchString.ToLower()));

            var allMoves = await _movies.FindAsync(filter, null, cancellationToken);

            return await (allMoves).ToListAsync();
        }

        public async Task<List<T>> GetAsync(CancellationToken cancellationToken = default)
        {
            var allMoves = await _movies.FindAsync(Builders<T>.Filter.Empty, null, cancellationToken);

            return await (allMoves).ToListAsync();
        }

        public async Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken = default) =>
            await (await _movies.FindAsync(movie => movie.Id == id)).FirstOrDefaultAsync();

        public async Task<T> CreateAsync(T movie, CancellationToken cancellationToken = default)
        {
            await _movies.InsertOneAsync(movie, null, cancellationToken);
            return movie;
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, T movieIn, CancellationToken cancellationToken = default) =>
            await _movies.ReplaceOneAsync(movie => movie.Id == id, movieIn, null, cancellationToken);

        public async Task<DeleteResult> RemoveAsync(T movieIn, CancellationToken cancellationToken = default) =>
            await _movies.DeleteOneAsync(movie => movie.Id == movieIn.Id, null, cancellationToken);

        public async Task<DeleteResult> RemoveAsync(string id, CancellationToken cancellationToken = default) =>
            await _movies.DeleteOneAsync(movie => movie.Id == id, cancellationToken);
    }
}