using ContractsDB;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDB
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RepositoryContext _context;

        public RoleRepository(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(IdentityRole Entity)
        {
            _context.Roles.Add(Entity);
        }

        public async Task<IEnumerable<string>> FindAll()
        {
            return await _context.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<IdentityRole> GetById(string Id)
        {
            return await _context.Roles.FindAsync(Id);
        }


    }
}
