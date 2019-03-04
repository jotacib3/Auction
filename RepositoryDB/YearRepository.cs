using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class YearRepository : RepositoryBase<Year>
    {
        public YearRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
