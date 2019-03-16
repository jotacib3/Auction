using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class EmployeeDataRepository : RepositoryBase<EmployeeData>, IEmployeeRepository
    {
        public EmployeeDataRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
