using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserStatusRepository : RepositoryAsync<UserStatus>, IUserStatusRepository 
    {
        public UserStatusRepository(DTSLocalDBContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<UserStatus> FindStatusById(int id)
        {
            var status = await FindByConditionAsync(u => u.Id == id);
            return status.FirstOrDefault() ?? throw new KeyNotFoundException();
        }

        public async Task<UserStatus> FindStatusByName(string name)
        {
            var status = await FindByConditionAsync(u => u.Name.Equals(name));
            return status.FirstOrDefault() ?? throw new KeyNotFoundException();
        }
    }
}
