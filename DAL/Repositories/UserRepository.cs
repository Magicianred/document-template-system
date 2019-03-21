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

        public async Task<IEnumerable<User>> FindUserByCondition(Expression<Func<User, bool>> expression)
        {
            var users = await DTSContext.User
                .Include(u => u.Status)
                .Include(u => u.Type)
                .Where(expression)
                .ToListAsync();
            return users.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }

        public async Task<User> FindByUserLogin(string login)
        {
            var users = await FindAllUsersAsync();
            var user = users
                .Where(u => u.Login.Equals(login));
            return user.FirstOrDefault() ?? throw new KeyNotFoundException("User not found");
        }

        public async Task<User> FindUserByIDAsync(int id)
        {
            var users = await FindAllUsersAsync();
            var user = users
                .Where(u => u.Id == id);
            return user.FirstOrDefault() ?? throw new KeyNotFoundException("User not found");
        }

        public async Task<bool> Exists(int id)
        {
            return await DTSContext.User.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> IsExistByLogin(string login)
        {
            return await DTSContext.User
                .AnyAsync(u => u.Login.Equals(login));
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

        public async Task DeleteAsync(User user)
        {
            Delete(user);
            await SaveAsync();
        }
    }
}
