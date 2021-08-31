using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> Get();
        Task<Movie> Get(string id);
        Task Save(Movie movie);
        Task Delete(Movie movie);
    }
}