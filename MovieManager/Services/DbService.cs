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
        private readonly IMongoCollection<T> _items;

        public DbService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _items = database.GetCollection<T>(settings.DatabaseCollectionName);
        }

        public async Task<List<T>> GetAsync(string searchString, string propertyName, CancellationToken cancellationToken = default)
        {
            // TODO move filter to appropriate class
            FilterDefinition<T> filter = string.IsNullOrEmpty(searchString) ? Builders<T>.Filter.Empty : Builders<T>.Filter.Regex(propertyName, new BsonRegularExpression(searchString, "i"));

            var allItems = await _items.FindAsync(filter, null, cancellationToken);

            return await (allItems).ToListAsync();
        }

        public async Task<List<T>> GetAsync(CancellationToken cancellationToken = default)
        {
            var allItems = await _items.FindAsync(Builders<T>.Filter.Empty, null, cancellationToken);

            return await (allItems).ToListAsync();
        }

        public async Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken = default) =>
            await (await _items.FindAsync(item => item.Id == id)).FirstOrDefaultAsync();

        public async Task<T> CreateAsync(T item, CancellationToken cancellationToken = default)
        {
            await _items.InsertOneAsync(item, null, cancellationToken);
            return item;
        }

        public async Task<ReplaceOneResult> UpdateAsync(string id, T itemIn, CancellationToken cancellationToken = default) =>
            await _items.ReplaceOneAsync(item => item.Id == id, itemIn, null, cancellationToken);

        public async Task<DeleteResult> RemoveAsync(T itemIn, CancellationToken cancellationToken = default) =>
            await _items.DeleteOneAsync(item => item.Id == itemIn.Id, null, cancellationToken);

        public async Task<DeleteResult> RemoveAsync(string id, CancellationToken cancellationToken = default) =>
            await _items.DeleteOneAsync(item => item.Id == id, cancellationToken);
    }
}