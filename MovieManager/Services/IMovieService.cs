using MongoDB.Driver;
using MovieManager.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieManager.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAsync(string searchString, CancellationToken cancellationToken = default);

        Task<Movie> GetDetailsAsync(string id, CancellationToken cancellationToken = default);

        Task<Movie> CreateAsync(Movie movie, CancellationToken cancellationToken = default);

        Task<ReplaceOneResult> UpdateAsync(string id, Movie movieIn, CancellationToken cancellationToken = default);

        Task<DeleteResult> RemoveAsync(Movie movieIn, CancellationToken cancellationToken = default);

        Task<DeleteResult> RemoveAsync(string id, CancellationToken cancellationToken = default);
    }
}