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

namespace MovieManager.Services.QueryHandlers
{
    public class MovieDetailsQueryHandler : IRequestHandler<MovieDetaisQuery, Movie>
    {
        private IDbService<Movie> _movieDbService;
        public MovieDetailsQueryHandler(IDbService<Movie> movieService)
        {
            _movieDbService = movieService;
        }

        public async Task<Movie> Handle(MovieDetaisQuery movieDetailsQuery, CancellationToken cancellationToken = default)
        {
            Check.NotNull(movieDetailsQuery, nameof(movieDetailsQuery));
            return await _movieDbService.GetDetailsAsync(movieDetailsQuery.DetailsId, cancellationToken).ConfigureAwait(false);
        }
    }
}
