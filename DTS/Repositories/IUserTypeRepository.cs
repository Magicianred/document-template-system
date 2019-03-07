using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    public interface IUserTypeRepository
    {
        Task<UserType> FindTypeById(int id);
    }
}
