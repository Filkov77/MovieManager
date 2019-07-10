using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;
using MovieManager.Services;

namespace MovieManager.Controllers
{
    public class MoviesController : Controller
    {
        // private readonly MovieManagerContext _context;
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, CancellationToken cancellationToken = default)
        {
            var movies = await _movieService.GetAsync(searchString, cancellationToken).ConfigureAwait(false);

            return View(movies);
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie, CancellationToken cancellationToken = default)
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie, CancellationToken cancellationToken = default)
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
        [HttpDelete, ActionName("Delete")]
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
