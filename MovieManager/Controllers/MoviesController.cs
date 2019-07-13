using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieManager.Models;
using MovieManager.Services;
using MovieManager.Services.Queries;

namespace MovieManager.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IDbService<Movie> _movieService;

        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator, ILogger<MoviesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting item list");
            var request = new MoviesQuery { QueryString = searchString };

            var movies = await _mediator.Send(request, cancellationToken);

            //TODO add paging with skip and limit

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

            var request = new MovieDetaisQuery { DetailsId = id };

            var movie = await _mediator.Send(request, cancellationToken);
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
                var request = new MovieCreateCommand { Movie = movie };

                var createdMovie = await _mediator.Send(request, cancellationToken);

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

            var request = new MovieDetaisQuery { DetailsId = id };
            var movie = await _mediator.Send(request, cancellationToken);

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
                // TODO handle concurrency errors
                var request = new MovieUpdateCommand { Movie = movie, Id = id };

                var updateResult = await _mediator.Send(request, cancellationToken);
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

            var request = new MovieDetaisQuery { DetailsId = id };
            var movie = await _mediator.Send(request, cancellationToken);

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
            var request = new MovieDeleteCommand { Id = id };

            var deleteResult = await _mediator.Send(request, cancellationToken);

            if (!deleteResult.IsAcknowledged)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
