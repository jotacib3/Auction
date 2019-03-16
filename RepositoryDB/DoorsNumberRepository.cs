using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class DoorsNumberRepository : RepositoryBase<DoorsNumber>, IDoorsNumberRepository
    {
        public DoorsNumberRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
