using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class PackRepository : RepositoryBase<Pack>
    {
        public PackRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
