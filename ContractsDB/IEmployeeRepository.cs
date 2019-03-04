using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractsDB
{
    public interface IEmployeeRepository : IRepositoryBase<EmployeeData> 
    {
    }
}
