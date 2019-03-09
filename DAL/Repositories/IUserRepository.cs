using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllUsersAsync();
        Task<User> FindUserByIDAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User oldUser);
        Task<bool> Exists(int id);
    }
}
