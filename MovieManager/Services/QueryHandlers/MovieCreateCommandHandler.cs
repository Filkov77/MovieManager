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
    public class MovieCreateCommandHandler : IRequestHandler<MovieCreateCommand, Movie>
    {
        private IDbService<Movie> _movieDbService;
        public MovieCreateCommandHandler(IDbService<Movie> movieService)
        {
            _movieDbService = movieService;
        }

        public async Task<Movie> Handle(MovieCreateCommand movieCreateCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(movieCreateCommand, nameof(movieCreateCommand));
            return await _movieDbService.CreateAsync(movieCreateCommand.Movie, cancellationToken);
        }
    }
}
