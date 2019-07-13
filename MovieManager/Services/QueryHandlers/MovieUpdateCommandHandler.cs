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
    public class MovieUpdateCommandHandler : IRequestHandler<MovieUpdateCommand, ReplaceOneResult>
    {
        private IDbService<Movie> _movieDbService;
        public MovieUpdateCommandHandler(IDbService<Movie> movieService)
        {
            _movieDbService = movieService;
        }

        public async Task<ReplaceOneResult> Handle(MovieUpdateCommand movieUpdateCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(movieUpdateCommand, nameof(movieUpdateCommand));
            return await _movieDbService.UpdateAsync(movieUpdateCommand.Id, movieUpdateCommand.Movie, cancellationToken).ConfigureAwait(false);
        }
    }
}
