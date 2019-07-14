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
    public class MoviesQueryHandler : IRequestHandler<MoviesQuery, IList<Movie>>
    {
        private IDbService<Movie> _movieDbService;
        public MoviesQueryHandler(IDbService<Movie> movieService)
        {
            _movieDbService = movieService;
        }

        public async Task<IList<Movie>> Handle(MoviesQuery moviesQuery, CancellationToken cancellationToken = default)
        {
            Check.NotNull(moviesQuery, nameof(moviesQuery));
            IList<Movie> movies;
            // TODO filter!!!
            if (string.IsNullOrEmpty(moviesQuery.QueryString))
            {
                movies = await _movieDbService.GetAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                movies = await _movieDbService.GetAsync(moviesQuery.QueryString, nameof(Movie.Title), cancellationToken).ConfigureAwait(false);
            }

            return movies;
        }
    }
}
