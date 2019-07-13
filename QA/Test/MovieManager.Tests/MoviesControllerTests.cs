using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieManager.Controllers;
using MovieManager.Models;
using MovieManager.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieManager.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfMovies()
        {
            var movie1 = new Movie { Director = "Director1", Id = "id1", Year = 1333, Actors = "Item1, Item2", Title = "Movie1", Image = "path1" };
            var movie2 = new Movie { Director = "Director2", Id = "id2", Year = 1112, Actors = "Item3, Item4", Title = "Movie2", Image = "path2" };
            var data = new List<Movie>
            {
                movie1,
                movie2
            };

            var mockMovieService = new Mock<IDbService<Movie>>();
            mockMovieService.Setup(m => m.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(data);

            var controller = new MoviesController(mockMovieService.Object, null, null);
            // Act
            var result = await controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
