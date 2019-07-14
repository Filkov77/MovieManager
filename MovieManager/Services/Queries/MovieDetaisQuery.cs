using MediatR;
using MovieManager.ViewModels;

namespace MovieManager.Services.Queries
{
    public class MovieDetaisQuery : IRequest<MovieViewModel>
    {
        public string DetailsId;
    }
}
