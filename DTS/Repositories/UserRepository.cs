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
        public UserRepository(DTSContext DtsContext)
            :base(DtsContext)
        {
        }

        public async Task<User> FindUserByIDAsync(int id)
        {
            var user = await DTSContext.Users
                .Include(u => u.Status)
                .Include(u => u.Type)
                .Where(u => u.ID == id)
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
    }
}
