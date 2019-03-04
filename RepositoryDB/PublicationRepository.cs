﻿using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class PublicationRepository : RepositoryBase<Publication>
    {
        public PublicationRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
