using MediatR;
using MovieManager.Models;
using MovieManager.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Services.Queries
{
    public class MoviesQuery : IRequest<IList<Movie>>
    {
        public string QueryString;
    }
}
