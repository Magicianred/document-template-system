using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
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
    }
}
