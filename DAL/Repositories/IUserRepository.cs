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
        Task<IEnumerable<User>> FindUserByCondition(Expression<Func<User, bool>> expression);
        Task<User> FindUserByIDAsync(int id);
        Task<User> FindByUserLogin(string login);
        Task<bool> Exists(int id);
        Task<bool> IsExistByLogin(string login);
        Task CreateAsync(User user);
        Task UpdateAsync(User User);
        Task DeleteAsync(User user);
    }
}
