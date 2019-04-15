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

        public async Task<IEnumerable<UserStatus>> FindAllUserStatuses()
        {
            var statuses = await FindAllAsync();
            return statuses.DefaultIfEmpty() ?? throw new KeyNotFoundException();
        }

        public async Task<UserStatus> FindUserStatusById(int id)
        {
            var status = await FindByConditionAsync(u => u.Id == id);
            return status.FirstOrDefault() ?? throw new KeyNotFoundException();
        }

        public async Task<UserStatus> FindUserStatusByName(string name)
        {
            var status = await FindByConditionAsync(u => u.Name.Equals(name));
            return status.FirstOrDefault() ?? throw new KeyNotFoundException();
        }
    }
}
