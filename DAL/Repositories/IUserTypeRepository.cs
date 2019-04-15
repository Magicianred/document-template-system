using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserTypeRepository
    {
        Task<IEnumerable<UserType>> FindAllUserTypes();
        Task<UserType> FindUserTypeById(int id);
        Task<UserType> FindUserTypeByName(string name);
    }
}
