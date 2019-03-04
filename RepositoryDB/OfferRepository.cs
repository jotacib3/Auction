using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class OfferRepository : RepositoryBase<Offer>
    {
        public OfferRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
