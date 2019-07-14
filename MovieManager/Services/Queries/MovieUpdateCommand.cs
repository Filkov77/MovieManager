using MediatR;
using MongoDB.Driver;
using MovieManager.Models;
using MovieManager.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Services.Queries
{
    public class MovieUpdateCommand : IRequest<ReplaceOneResult>
    {
        public Movie Movie;
        public string Id;
    }
}
