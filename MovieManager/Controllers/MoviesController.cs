using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieManager.Models;
using MovieManager.Services;

namespace MovieManager.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IDbService<Movie> _movieService;

        public MoviesController(IDbService<Movie> movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting item list");
            //TODO add paging with skip limit
            IList<Movie> movies;
            if (string.IsNullOrEmpty(searchString))
            {
                movies = await _movieService.GetAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                movies = await _movieService.GetAsync(searchString, nameof(Movie.Title), cancellationToken).ConfigureAwait(false);
            }

            return View(movies);
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            _logger.LogError($"Error: {errorModel.ToString()}");
            return View(errorModel);
        }

        // GET: Movies/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetDetailsAsync(id, cancellationToken).ConfigureAwait(false);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,Actors,Image, Year")] Movie movie, CancellationToken cancellationToken = default)
        {
            if (ModelState.IsValid)
            {
                var createdMovie = await _movieService.CreateAsync(movie, cancellationToken).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetDetailsAsync(id, cancellationToken).ConfigureAwait(false);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Director,Actors,Image, Year")] Movie movie, CancellationToken cancellationToken = default)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TODO handle concurrency
                var updateResult = await _movieService.UpdateAsync(id, movie, cancellationToken).ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO handle errors
            var movie = await _movieService.GetDetailsAsync(id, cancellationToken).ConfigureAwait(false);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // DELETE: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                return NotFound();
            }

            // TODO handle errors
            var deleteResult = await _movieService.RemoveAsync(id, cancellationToken).ConfigureAwait(false);
            if (!deleteResult.IsAcknowledged)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
