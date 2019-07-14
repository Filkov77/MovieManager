using System.Threading;
using System.Threading.Tasks;
using MovieManager.Services.Queries;
using MovieManager.Infrastructure.Exceptions;
using MediatR;
using MovieManager.ViewModels;
using AutoMapper;
using MovieManager.Services.DomainModels;

namespace MovieManager.Services.QueryHandlers
{
    public class MovieCreateCommandHandler : IRequestHandler<MovieCreateCommand, MovieViewModel>
    {
        private IDbService<DbMovie> _movieDbService;
        private IMapper _mapper;
        public MovieCreateCommandHandler(IDbService<DbMovie> movieService, IMapper mapper)
        {
            _movieDbService = movieService;
            _mapper = mapper;
        }

        public async Task<MovieViewModel> Handle(MovieCreateCommand movieCreateCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(movieCreateCommand, nameof(movieCreateCommand));
            var dbMovie = _mapper.Map<DbMovie>(movieCreateCommand.MovieViewModel);
            await _movieDbService.CreateAsync(dbMovie, cancellationToken);
            return movieCreateCommand.MovieViewModel;
        }
    }
}
