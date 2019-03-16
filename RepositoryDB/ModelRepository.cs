using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class ModelRepository : RepositoryBase<Model>, IModelRepository
    {
        public ModelRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
