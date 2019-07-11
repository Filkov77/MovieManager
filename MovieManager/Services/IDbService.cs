using MongoDB.Driver;
using MovieManager.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieManager.Services
{
    public interface IDbService<T> where T : IIdentifiable
    {
        Task<List<T>> GetAsync(CancellationToken cancellationToken = default);
        Task<List<T>> GetAsync(string searchString, string propertyName, CancellationToken cancellationToken = default);

        Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken = default);

        Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);

        Task<ReplaceOneResult> UpdateAsync(string id, T itemIn, CancellationToken cancellationToken = default);

        Task<DeleteResult> RemoveAsync(T itemIn, CancellationToken cancellationToken = default);

        Task<DeleteResult> RemoveAsync(string id, CancellationToken cancellationToken = default);
    }
}