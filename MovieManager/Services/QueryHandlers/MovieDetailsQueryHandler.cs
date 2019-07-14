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
    public class MovieDetailsQueryHandler : IRequestHandler<MovieDetaisQuery, MovieViewModel>
    {
        private IDbService<DbMovie> _movieDbService;
        private IMapper _mapper;
        public MovieDetailsQueryHandler(IDbService<DbMovie> movieService, IMapper mapper)
        {
            _movieDbService = movieService;
            _mapper = mapper;
        }

        public async Task<MovieViewModel> Handle(MovieDetaisQuery movieDetailsQuery, CancellationToken cancellationToken = default)
        {
            //TODO map id
            Check.NotNull(movieDetailsQuery, nameof(movieDetailsQuery));
            var dbMovie = await _movieDbService.GetDetailsAsync(movieDetailsQuery.DetailsId, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<MovieViewModel>(dbMovie);
        }
    }
}
