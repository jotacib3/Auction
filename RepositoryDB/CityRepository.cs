using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class CityRepository : RepositoryBase<City>
    {
        public CityRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
