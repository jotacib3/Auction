using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class BrandRepository : RepositoryBase<Brand>
    {
        public BrandRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
