using MovieManager.Models;
using MovieManager.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MovieManager.Services;
using MovieManager.Services.Queries;
using MovieManager.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Driver;

namespace MovieManager.Services.QueryHandlers
{
    public class MovieDeleteCommandHandler : IRequestHandler<MovieDeleteCommand, DeleteResult>
    {
        private IDbService<Movie> _movieDbService;
        public MovieDeleteCommandHandler(IDbService<Movie> movieService)
        {
            _movieDbService = movieService;
        }

        public async Task<DeleteResult> Handle(MovieDeleteCommand movieDeleteCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(movieDeleteCommand, nameof(movieDeleteCommand));
            return await _movieDbService.RemoveAsync(movieDeleteCommand.Id, cancellationToken).ConfigureAwait(false);
        }
    }
}
