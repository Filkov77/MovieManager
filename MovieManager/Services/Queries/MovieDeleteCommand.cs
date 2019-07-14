using MediatR;
using MongoDB.Driver;
using MovieManager.ViewModels;

namespace MovieManager.Services.Queries
{
    public class MovieDeleteCommand : IRequest<DeleteResult>
    {
        public string Id;
    }
}
