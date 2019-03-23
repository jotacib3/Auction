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
            this._context = context;
        }

        public void Delete(User Entity)
        {
           this._context.Entry(Entity).State = EntityState.Deleted;
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            return await this._context.Users.ToListAsync();
        }

        public async Task<User> GetById(string Id)
        {
            return await this._context.Users.FindAsync(Id);
        }

        public void Update(User Entity)
        {
           this. _context.Entry(Entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<User>> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return await this._context.Set<User>().Where(expression).ToListAsync();
            
        }
        public IQueryable<User> UsersQueryable()
        {
             return this._context.Users.AsQueryable();
        }

        public async Task<IQueryable<User>> FindAllInRole( string roleId)
        {
            var userRole = await _context.UserRoles.Where(u => u.RoleId.Equals(roleId)).ToListAsync();
            IQueryable<User> users=this._context.Users;
            foreach (var ur in userRole)
                users=_context.Users.Where(a => a.Id.Equals(ur.UserId));

            return users;
            
        }


    }
}
