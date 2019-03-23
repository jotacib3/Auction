using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContractsDB
{
    public interface IUserRepository
    {
        IQueryable<User> UsersQueryable();
        Task<IEnumerable<User>> FindAll();
        Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression);
        Task<User> GetById(string Id);
        void Update(User Entity);
        void Delete(User Entity);
    }
}
