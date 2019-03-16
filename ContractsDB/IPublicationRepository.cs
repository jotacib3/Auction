using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace ContractsDB
{
    public interface IPublicationRepository: IRepositoryBase<Publication>
    {
    }
}
