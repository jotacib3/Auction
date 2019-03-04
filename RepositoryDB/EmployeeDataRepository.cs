using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class EmployeeDataRepository : RepositoryBase<EmployeeData>
    {
        public EmployeeDataRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
