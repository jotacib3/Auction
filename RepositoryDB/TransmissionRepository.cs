using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class TransmissionRepository : RepositoryBase<Transmission>, ITransmissionRepository
    {
        public TransmissionRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
