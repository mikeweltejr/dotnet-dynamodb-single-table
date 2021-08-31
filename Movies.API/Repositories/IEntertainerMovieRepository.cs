using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IEntertainerMovieRepository
    {
        Task<List<MovieEntertainer>> Get(string entertainerId);
    }
}