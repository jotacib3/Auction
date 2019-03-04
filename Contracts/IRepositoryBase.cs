using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities;

namespace Contracts
{
    public interface IRepositoryBase<T> where T : IEntity
    {
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetById(Guid Id);
        void Create(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}

