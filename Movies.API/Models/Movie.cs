using System;

namespace Movies.API.Models
{
    public class Movie
    {
        public string PK { get; set; }
        public string SK { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Year { get; set; }

        public Movie()
        {
            PK = SortKeyPrefixes.MOVIE;
            SK = SortKeyPrefixes.MOVIE + Id;
        }
    }
}