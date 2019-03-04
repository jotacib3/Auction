﻿using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class LocationRepository : RepositoryBase<Location>
    {
        public LocationRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
