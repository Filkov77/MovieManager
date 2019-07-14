using MediatR;
using MovieManager.ViewModels;
using System.Collections.Generic;

namespace MovieManager.Services.Queries
{
    public class MoviesQuery : IRequest<IList<MovieViewModel>>
    {
        public string QueryString;
    }
}
