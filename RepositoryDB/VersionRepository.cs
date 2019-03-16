using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class VersionRepository : RepositoryBase<Version>, IVersionRepository
    {
        public VersionRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
