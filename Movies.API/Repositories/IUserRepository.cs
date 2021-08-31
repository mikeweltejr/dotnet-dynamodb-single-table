using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task<User> Get(string id);
        Task Save(User user);
        Task Delete(User user);
    }
}