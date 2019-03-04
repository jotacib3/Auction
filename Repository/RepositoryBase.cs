using Contracts;
using Entities;
using Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> :  IRepositoryBase<T> where T : class,IEntity,new()
    {
        protected RepositoryContext _context { get; set; }
        public RepositoryBase(RepositoryContext context)
        {
            this._context = context;
        }
        public DbSet<T> Get()
        {
            return this._context.Set<T>();
        }
        public async Task<IEnumerable<T>> FindAll()
        {
            return await this._context.Set<T>().ToListAsync();
        }
        
        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await this._context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {
            var entity =await FindByCondition(e => e.Id.Equals(Id));
            return entity.DefaultIfEmpty(new T()).FirstOrDefault();
        }

        public void Create(T Entity)
        {
            this._context.Set<T>().Add(Entity);
        }

        public void Update(T Entity)
        {
            this._context.Entry(Entity).State = EntityState.Modified;
        }

        public void Delete(T Entity)
        {
            this._context.Set<T>().Remove(Entity);
        }


    }
}
