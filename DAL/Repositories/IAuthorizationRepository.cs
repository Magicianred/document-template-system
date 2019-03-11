using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAuthorizationRepository
    {
        Task<IEnumerable<Authorization>> FindAllAuthorizations();
        Task<Authorization> FindByUserLogin(string login);
        Task<bool> IsExistByLogin(string login);
    }
}
