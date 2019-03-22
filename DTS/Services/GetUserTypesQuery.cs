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
        IQueryHandlerAsync<GetUserTypesQuery, IList<string>>
    {
        private IRepositoryWrapper repository;

        public GetUserTypesQueryHandler(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        public async Task<IList<string>> HandleAsync(GetUserTypesQuery query)
        {
            var types = await repository.UserType.FindAllUserTypes();
            IList<string> typesNames = new List<string>();
            foreach (var type in types)
            {
                typesNames.Add(type.Name);
            }
            return typesNames;
        }
    }
}
