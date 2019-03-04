using Contracts;
using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext context;

        public UnitOfWork(RepositoryContext context)
        {
            this.context = context;
        }
        public async Task SaveChangesAsync()
        {
           await context.SaveChangesAsync();
        }
    }
}
