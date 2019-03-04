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

        public async Task<User> FindByIDAsync(int id)
        {
            var user = await FindByConditionAsync(u => u.ID == id);
            return user.DefaultIfEmpty(new User()).FirstOrDefault();
        }

        public async Task CreateAsync(User user)
        {
            Create(user);
            await SaveAsync();
        }

        public async Task UpdateAsync(User oldUser, User user)
        {
            oldUser.Login = user.Login;
            oldUser.Password = user.Password;
            oldUser.Name = user.Name;
            oldUser.Surname = user.Surname;
            oldUser.Type = user.Type;
            oldUser.Status = user.Status;

            Update(oldUser);
            await SaveAsync();
        }
    }
}
