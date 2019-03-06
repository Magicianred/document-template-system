using DTS.Data;
using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public class UserTypeRepository : RepositoryAsync<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(DTSContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<UserType> FindTypeById(int id)
        {
            var type = await FindByConditionAsync(t => t.ID == id);
            return type.FirstOrDefault() ?? new UserType();
        }
    }
}
