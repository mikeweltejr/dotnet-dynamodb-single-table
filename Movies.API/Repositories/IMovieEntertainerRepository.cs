using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IMovieEntertainerRepository
    {
        Task<List<MovieEntertainer>> Get(string pk);
        Task<MovieEntertainer> Get(string pk, string entertainerId);
        Task Save(MovieEntertainer movieEntertainer);
        Task Delete(MovieEntertainer movieEntertainer);
    }
}