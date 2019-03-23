using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContractsDB
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> FindAll();
        void Create(IdentityRole Entity);
        Task<IdentityRole> GetById(string Id);
    }
}
