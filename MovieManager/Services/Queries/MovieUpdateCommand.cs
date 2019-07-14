using MediatR;
using MongoDB.Driver;
using MovieManager.ViewModels;

namespace MovieManager.Services.Queries
{
    public class MovieUpdateCommand : IRequest<ReplaceOneResult>
    {
        public MovieViewModel MovieViewModel;
        public string Id;
    }
}
