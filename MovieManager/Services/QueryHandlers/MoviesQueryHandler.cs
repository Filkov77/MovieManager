using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MovieManager.Services.Queries;
using MovieManager.Infrastructure.Exceptions;
using MediatR;
using MovieManager.Services.DomainModels;
using MovieManager.ViewModels;
using AutoMapper;

namespace MovieManager.Services.QueryHandlers
{
    public class MoviesQueryHandler : IRequestHandler<MoviesQuery, IList<MovieViewModel>>
    {
        private IDbService<DbMovie> _movieDbService;
        private IMapper _mapper;

        public MoviesQueryHandler(IDbService<DbMovie> movieService, IMapper mapper)
        {
            _movieDbService = movieService;
            _mapper = mapper;
        }

        public async Task<IList<MovieViewModel>> Handle(MoviesQuery moviesQuery, CancellationToken cancellationToken = default)
        {
            Check.NotNull(moviesQuery, nameof(moviesQuery));
            IList<DbMovie> movies;
            // TODO filter!!!
            if (string.IsNullOrEmpty(moviesQuery.QueryString))
            {
                movies = await _movieDbService.GetAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                movies = await _movieDbService.GetAsync(moviesQuery.QueryString, nameof(DbMovie.Title), cancellationToken).ConfigureAwait(false);
            }

            return _mapper.Map<List<MovieViewModel>>(movies);
        }
    }
}
