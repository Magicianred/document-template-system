using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    interface IRepositoryAsync<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> FindByID(int id);
        void Create(T entity);
        void Update(T entity);
        Task Save();
    }
}
