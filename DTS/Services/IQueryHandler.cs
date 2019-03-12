using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTS.Services
{
    public interface IQueryHandlerAsync<T, R> where T : IQuery
    {
        Task<R> HandleAsync(T query);
    }
}
