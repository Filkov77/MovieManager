using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using MovieManager.Controllers;
using MovieManager.Models;
using MovieManager.Services;
using System;
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
            var movie1 = new Movie { Genre = "Comedy", Id = "id1", Price = 2.22m, ReleaseDate = DateTime.Now, Title = "Movie1" };
            var movie2 = new Movie { Genre = "Comedy", Id = "id2", Price = 2.22m, ReleaseDate = DateTime.Now, Title = "Movie2" };
            var data = new List<Movie>
            {
                movie1,
                movie2
            };

            Mock<DbSet<Movie>> mock = data.AsQueryable().BuildMockDbSet();

            var mockMovieService = new Mock<IMovieService>();
            mockMovieService.Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(data);

            var controller = new MoviesController(mockMovieService.Object);
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
