using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserTypeRepository : RepositoryAsync<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(DTSLocalDBContext DtsContext)
            : base(DtsContext)
        {
        }

        public async Task<UserType> FindTypeById(int id)
        {
            var type = await FindByConditionAsync(t => t.Id == id);
            return type.FirstOrDefault() ?? throw new KeyNotFoundException();
        }
    }
}
