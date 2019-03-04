using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task<User> FindByIDAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User oldUser, User user);
    }
}
