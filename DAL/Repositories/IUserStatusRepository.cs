using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserStatusRepository
    {
        Task<UserStatus> FindStatusById(int id); 
    }
}
