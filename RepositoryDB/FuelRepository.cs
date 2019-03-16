using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class FuelRepository : RepositoryBase<Fuel>, IFuelRepository
    {
        public FuelRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
