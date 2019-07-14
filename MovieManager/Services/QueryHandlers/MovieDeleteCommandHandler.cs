using System.Threading;
using System.Threading.Tasks;
using MovieManager.Services.Queries;
using MovieManager.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Driver;
using MovieManager.Services.DomainModels;

namespace MovieManager.Services.QueryHandlers
{
    public class MovieDeleteCommandHandler : IRequestHandler<MovieDeleteCommand, DeleteResult>
    {
        private IDbService<DbMovie> _movieDbService;

        public MovieDeleteCommandHandler(IDbService<DbMovie> movieService)
        {
            _movieDbService = movieService;
        }

        public async Task<DeleteResult> Handle(MovieDeleteCommand movieDeleteCommand, CancellationToken cancellationToken = default)
        {
            //TODO Think about mapping only id
            Check.NotNull(movieDeleteCommand, nameof(movieDeleteCommand));

            return await _movieDbService.RemoveAsync(movieDeleteCommand.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}
