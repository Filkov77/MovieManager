using MediatR;
using MovieManager.ViewModels;

namespace MovieManager.Services.Queries
{
    public class MovieCreateCommand : IRequest<MovieViewModel>
    {
        public MovieViewModel MovieViewModel;
    }
}
