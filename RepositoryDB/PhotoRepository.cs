using ContractsDB;
using Data;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDB
{
    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(RepositoryContext context):base(context)
        {
        }
    }
}
