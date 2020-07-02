using Entities.Models;
using Entities.ModelsView;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthRepo
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(RegisterModel model);
        Task<IdentityResult> RegisterEmployee(RegisterEmployeeModel model);
        Task<User> Login(LoginModel model);
        Task<IdentityResult> AddRole(IdentityRole role);
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        //Task<IdentityResult> SetPassword(SetPasswordModel model);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
        void ForgotPassword(ForgotPasswordModel model);
        Task<User> UserExists(string Username);
        Task<IList<string>> GetAllRoles(User user);
        Task<IdentityResult> AddRole(string id, string rolname);
        Task<IdentityResult> RemoveRole(string id, string rolname);
        Task<IEnumerable<User>> GetUsersInRole(string rol);
    } 
}
