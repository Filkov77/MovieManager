using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using MovieManager.Controllers;
using MovieManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieManager.Tests
{
    public class MoviesServicesTests
    {

        [Fact]
        public async Task GetAsyncListOfMovies_Success()
        {
            var movie1 = new Movie { Director = "Director1", Id = "id1", Year = 1333, Actors = "Item1, Item2", Title = "Movie1", Image = "path1" };
            var movie2 = new Movie { Director = "Director2", Id = "id2", Year = 1112, Actors = "Item3, Item4", Title = "Movie2", Image = "path2" };
            var data = new List<Movie>
            {
                movie1,
                movie2
            };

            var mockMovieService = new Mock<IMongoCollection<Movie>>();
            // mockMovieService.Setup(collection => collection.FindAsync(It.IsAny<FilterDefinition<Movie>>(), It.IsAny<FindOptions>(), It.IsAny<CancellationToken>())).Returns(data).ReturnsAsync(data);


        }
    }
}
