using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.API.Services
{
    public sealed class GetUserTypesQuery : IQuery
    {
    }

    public sealed class GetUserTypesQueryHandler :
        IQueryHandlerAsync<GetUserTypesQuery, IEnumerable<string>>
    {
        private IRepositoryWrapper repository;

        public GetUserTypesQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<string>> HandleAsync(GetUserTypesQuery query)
        {
            var types = await repository.UserType.FindAll();
            IList<string> typesNames = new List<string>();
            foreach (var type in types)
            {
                typesNames.Add(type.Name);
            }
            return typesNames;
        }
    }
}
