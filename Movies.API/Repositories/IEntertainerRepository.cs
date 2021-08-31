using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IEntertainerRepository
    {
        Task<List<Entertainer>> Get();
        Task<Entertainer> Get(string id);
        Task Save(Entertainer entertainer);
        Task Delete(Entertainer entertainer);
    }
}