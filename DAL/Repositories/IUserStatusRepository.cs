using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserStatusRepository
    {
        Task<IEnumerable<UserStatus>> FindAllUserStatuses();
        Task<UserStatus> FindUserStatusById(int id);
        Task<UserStatus> FindUserStatusByName(string name);
    }
}
