using System.Threading;
using System.Threading.Tasks;
using MovieManager.Services.Queries;
using MovieManager.Infrastructure.Exceptions;
using MediatR;
using MongoDB.Driver;
using MovieManager.Services.DomainModels;
using AutoMapper;

namespace MovieManager.Services.QueryHandlers
{
    public class MovieUpdateCommandHandler : IRequestHandler<MovieUpdateCommand, ReplaceOneResult>
    {
        private IDbService<DbMovie> _movieDbService;
        private IMapper _mapper;
        public MovieUpdateCommandHandler(IDbService<DbMovie> movieService, IMapper mapper)
        {
            _movieDbService = movieService;
            _mapper = mapper;
        }

        public async Task<ReplaceOneResult> Handle(MovieUpdateCommand movieUpdateCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(movieUpdateCommand, nameof(movieUpdateCommand));

            var dbMovie = _mapper.Map<DbMovie>(movieUpdateCommand.MovieViewModel);
            return await _movieDbService.UpdateAsync(movieUpdateCommand.Id, dbMovie, cancellationToken).ConfigureAwait(false);
        }
    }
}
