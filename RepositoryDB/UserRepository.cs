using ContractsDB;
using Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDB
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _context;

        public UserRepository(RepositoryContext context)
        {
            _context = context;
        }

        public void Delete(User Entity)
        {
            _context.Entry(Entity).State = EntityState.Deleted;
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(string Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public void Update(User Entity)
        {
            _context.Entry(Entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return await this._context.Set<User>().Where(expression).ToListAsync();
        }
        public IQueryable<User> UsersQueryable()
        {
             return _context.Users.AsQueryable();
        }
    }
}
