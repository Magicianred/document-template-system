using DTS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DTS.Data;
using Microsoft.EntityFrameworkCore;

namespace DTS
{
    public abstract class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected DTSContext DTSContext { get; set; }

        public RepositoryAsync(DTSContext DtsContext)
        {
            this.DTSContext = DtsContext;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.DTSContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.DTSContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await this.DTSContext.SaveChangesAsync();
        }

        public void Create(T entity)
        {
            this.DTSContext.Set<T>().Add(entity);
        }
        
        public void Update(T entity)
        {
            this.DTSContext.Set<T>().Update(entity);
        }
    }
}
