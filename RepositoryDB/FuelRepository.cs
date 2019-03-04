using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class FuelRepository : RepositoryBase<Fuel>
    {
        public FuelRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
