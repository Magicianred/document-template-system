using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        public UserRepository(DTSLocalDBContext DtsContext)
            :base(DtsContext)
        {
        }

        public async Task<IEnumerable<User>> FindAllUsersAsync()
        {
            var users = await DTSContext.User
                .Include(u => u.Status)
                .Include(u => u.Type)
                .ToListAsync();

            return users.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }

        public async Task<User> FindUserByIDAsync(int id)
        {
            var users = await FindAllUsersAsync();
            var user = users
                .Where(u => u.Id == id);
            return user.FirstOrDefault() ?? throw new KeyNotFoundException();
        }

        public async Task CreateAsync(User user)
        {
            Create(user);
            await SaveAsync();
        }

        public async Task UpdateAsync(User user)
        {
            Update(user);
            await SaveAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await DTSContext.User.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<User>> FindUserByCondition(Expression<Func<User, bool>> expression)
        {
            var users = await FindByConditionAsync(expression);
            return users.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }
    }
}
