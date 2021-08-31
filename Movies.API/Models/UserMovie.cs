namespace Movies.API.Models
{
    public class UserMovie
    {
        public string PK { get; set; }
        public string SK { get; set; }
        private string _userId;
        public string UserId {
            get { return _userId; }
            set
            {
                _userId = value;
                PK = value;
            }
        }
        private string _movieId;
        public string MovieId {
            get { return _movieId; }
            set
            {
                _movieId = value;
                SK = SortKeyPrefixes.USER_MOVIE + value;
            }
        }
        public string UserName { get; set; }
        public string MovieName { get; set; }
    }
}