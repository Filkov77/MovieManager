using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using MovieManager.Controllers;
using MovieManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MovieManager.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfMovies()
        {
            var options = new DbContextOptionsBuilder<MovieManagerContext>()
            .UseInMemoryDatabase(databaseName: "MovieListDatabase")
            .Options;

            var movie1 = new Movie { Genre = "Comedy", Id = 1, Price = 2.22m, ReleaseDate = DateTime.Now, Title = "Movie1" };
            var movie2 = new Movie { Genre = "Comedy", Id = 2, Price = 2.22m, ReleaseDate = DateTime.Now, Title = "Movie2" };
            var data = new List<Movie>
            {
                movie1,
                movie2
            };

            var mock = data.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<MovieManagerContext>(options);
            mockContext.Setup(m => m.Movie).Returns(mock.Object);

            var controller = new MoviesController(mockContext.Object);
            // Act
            var result = await controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Movie>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private static Mock<DbSet<Movie>> GetQueryableMockMovieDbSet()
        {
            var movie1 = new Movie { Genre = "Comedy", Id = 1, Price = 2.22m, ReleaseDate = DateTime.Now, Title = "Movie1" };
            var movie2 = new Movie { Genre = "Comedy", Id = 2, Price = 2.22m, ReleaseDate = DateTime.Now, Title = "Movie1" };

            var data = new List<Movie> { movie1, movie2 };

            var mockDocumentDbSet = new Mock<DbSet<Movie>>();
            mockDocumentDbSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockDocumentDbSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockDocumentDbSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockDocumentDbSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockDocumentDbSet.Setup(m => m.Add(It.IsAny<Movie>())).Callback<Movie>(data.Add);
            return mockDocumentDbSet;
        }
    }
}
