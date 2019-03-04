using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class ModelRepository : RepositoryBase<Model>
    {
        public ModelRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
