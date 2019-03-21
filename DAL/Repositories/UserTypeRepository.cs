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

        public async Task<IEnumerable<UserType>> FindAllUserTypes()
        {
            var roles = await FindAllAsync();
            return roles.DefaultIfEmpty() ?? throw new KeyNotFoundException();
        }

        public async Task<UserType> FindUserTypeById(int id)
        {
            var type = await FindByConditionAsync(t => t.Id == id);
            return type.FirstOrDefault() ?? throw new KeyNotFoundException();
        }

        public async Task<UserType> FindUserTypeByName(string name)
        {
            var type = await FindByConditionAsync(t => t.Name.Equals(name));
            return type.FirstOrDefault() ?? throw new KeyNotFoundException();
        }
    }
}
