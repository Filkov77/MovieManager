using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieManager.ViewModels
{
    public class MovieViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Director { get; set; }

        public string Actors { get; set; }

        public IList<string> ActorList { get; set; }

        public string Image { get; set; }

        public int Year { get; set; }
    }
}
