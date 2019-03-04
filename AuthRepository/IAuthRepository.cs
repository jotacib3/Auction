using Entities.Models;
using Entities.ModelsView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthRepo
{
    public interface IAuthRepository
    {
        Task<bool> Register(RegisterModel model);
        Task<User> Login(LoginModel model);
        Task<bool> UserExists(string Username);
        Task<IList<string>> GetAllRoles(User user);
    }
}
