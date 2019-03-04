using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class StateRepository : RepositoryBase<State>
    {
        public StateRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
