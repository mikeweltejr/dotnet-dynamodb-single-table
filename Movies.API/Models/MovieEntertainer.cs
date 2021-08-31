namespace Movies.API.Models
{
    public class MovieEntertainer
    {
        public string PK { get; set; }
        public string SK { get; set; }
        private string _movieId;
        public string Movieid { 
            get { return _movieId; }
            set
            {
                _movieId = value;
                PK = value;
            }
        }
        private string _entertainerId;
        public string Entertainerid {
            get { return _entertainerId; }
            set
            {
                _entertainerId = value;
                SK = SortKeyPrefixes.MOVIE_ENT + value;
                GSI_1 = value;
            }
        }
        public string MovieName { get; set; }
        public string EntertainerName { get; set; }
        public string GSI_1 { get; set; }
    }
}