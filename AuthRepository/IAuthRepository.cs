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
        Task<IdentityResult> ChangePassword(ChangePasswordModel model, ClaimsPrincipal user);
        Task<IdentityResult> SetPassword(SetPasswordModel model, ClaimsPrincipal user);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model, ClaimsPrincipal user);
        void ForgotPassword(ForgotPasswordModel model, ClaimsPrincipal user);
        Task<bool> UserExists(string Username);
        Task<IList<string>> GetAllRoles(User user);
    }
}
