using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieManager.Models
{
    public class MovieManagerContext : DbContext
    {
        public MovieManagerContext (DbContextOptions<MovieManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovieManager.Models.Movie> Movie { get; set; }
    }
}
