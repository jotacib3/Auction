using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContractsDB
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAll();
        Task<User> GetById(string Id);
        void Update(User Entity);
        void Delete(User Entity);
        void Create(User Entity);
    }
}
