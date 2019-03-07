using DTS.Data;
using DTS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        public UserRepository(DTSLocalDBContext DtsContext)
            :base(DtsContext)
        {
        }

        public async Task<User> FindUserByIDAsync(int id)
        {
            var user = await DTSContext.User
                .Include(u => u.Status)
                .Include(u => u.Type)
                .Where(u => u.Id == id)
                .SingleOrDefaultAsync();
            return user ?? new User();
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

        public async Task<IEnumerable<User>> FindAllUsersAsync()
        {
            return await FindAllAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await DTSContext.User.AnyAsync(e => e.Id == id);
        }

    }
}
