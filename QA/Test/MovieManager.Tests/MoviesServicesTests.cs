using MongoDB.Driver;
using Moq;
using MovieManager.Services.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieManager.Tests
{
    public class MoviesServicesTests
    {

        [Fact]
        public async Task GetAsyncListOfMovies_Success()
        {
            var movie1 = new DbMovie { Director = "Director1", Id = "id1", Year = 1333, Actors = "Item1, Item2", Title = "Movie1", Image = "path1" };
            var movie2 = new DbMovie { Director = "Director2", Id = "id2", Year = 1112, Actors = "Item3, Item4", Title = "Movie2", Image = "path2" };
            var data = new List<DbMovie>
            {
                movie1,
                movie2
            };

            var mockMovieService = new Mock<IMongoCollection<DbMovie>>();
            // mockMovieService.Setup(collection => collection.FindAsync(It.IsAny<FilterDefinition<Movie>>(), It.IsAny<FindOptions>(), It.IsAny<CancellationToken>())).Returns(data).ReturnsAsync(data);


        }
    }
}
