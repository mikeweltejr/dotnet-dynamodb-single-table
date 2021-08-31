using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IUserMovieRepository
    {
        Task<List<UserMovie>> Get(string pk);
        Task<UserMovie> Get(string pk, string movieId);
        Task Delete(UserMovie userMovie);
        Task Save(UserMovie userMovie);
    }
}