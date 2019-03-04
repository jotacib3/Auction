using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class DoorsNumberRepository : RepositoryBase<DoorsNumber>
    {
        public DoorsNumberRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
