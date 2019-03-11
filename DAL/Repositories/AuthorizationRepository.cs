using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    class AuthorizationRepository : RepositoryAsync<Authorization>, IAuthorizationRepository
    {
        public AuthorizationRepository(DTSLocalDBContext DtsContext)
            : base(DtsContext) 
        {
        }

        public async Task<IEnumerable<Authorization>> FindAllAuthorizations()
        {
            var authorizations = await DTSContext.Authorizations
                .Include(a => a.Status)
                .Include(a => a.user)
                .ToListAsync();
            return authorizations.DefaultIfEmpty() ?? throw new InvalidOperationException();
        }

        public async Task<Authorization> FindByUserLogin(string login)
        {
            var authoriazation = await DTSContext.Authorizations
                .Include(a => a.Status)
                .Include(a => a.user)
                .Where(a => a.Login.Equals(login))
                .FirstOrDefaultAsync();

            return authoriazation ?? throw new KeyNotFoundException();
        }

        public async Task<bool> IsExistByLogin(string login)
        {
            return await DTSContext.Authorizations
                .AnyAsync(a => a.Login.Equals(login));
        }
    }
}
