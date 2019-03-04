using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task<IEnumerable<User>> FindByConditionAsync(Expression<Func<User, bool>> expression);
        Task<User> FindByIDAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User oldUser, User user);
    }
}
