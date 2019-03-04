using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DTS.Repositories
{
    interface IRepositoryAsync<T>
    {
        Task<ICollection<T>> FindAllAsync();
        Task<ICollection<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> FindByID(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Save();
    }
}
