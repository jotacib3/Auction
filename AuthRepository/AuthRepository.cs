using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        { 
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IList<string>> GetAllRoles(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<User> Login(LoginModel model)
        {
            var userToVerify = await userManager.FindByNameAsync(model.UserName);
            if (userToVerify == null)
                return null;
            if (!await userManager.CheckPasswordAsync(userToVerify, model.Password))
                return null;

            return userToVerify;

        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = new User() { UserName = model.Email, Email = model.Email, Enabled = true };

            var result = await userManager.CreateAsync(user, model.Password);

            if (model.Role != null)
            {
                var roleresult = userManager.AddToRoleAsync(user, model.Role);
            }
            
            return result;
        }

        public async Task<IdentityResult> RegisterEmployee(RegisterEmployeeModel model)
        {
            var user = new User() { UserName = model.Email,
                                   Email = model.Email, Enabled = null, EmployeeData = model.Employee };

            var result = await userManager.CreateAsync(user, model.Password);
            var roleresult = userManager.AddToRoleAsync(user, model.Role);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword,
                model.NewPassword);

            return result;
        }

        public async void ForgotPassword(ForgotPasswordModel model, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            string code = await userManager.GeneratePasswordResetTokenAsync(user);
           // await userManager.Se(user, "Reset Password", $"Please reset your password by using this {code}");
        }
        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            var result = await userManager.ResetPasswordAsync(user,model.Code,model.Password);

            return result;
        }

        public async Task<IdentityResult> SetPassword(SetPasswordModel model, ClaimsPrincipal _user)
        {
            var user = await userManager.GetUserAsync(_user);
            var result = await userManager.AddPasswordAsync(user, model.NewPassword);

            return result;
        }


        public async Task<bool> UserExists(string Username)
        {
            return  await userManager.FindByNameAsync(Username) != null; 
        }

        public async Task<IdentityResult> AddRole(string id, string rol)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.AddToRoleAsync(user, rol);

            return result;
        }

        public async Task<IdentityResult> RemoveRole(string id, string rol)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, rol);

            return result;
        }

        public async Task<IEnumerable<User>> GetUsersInRole(string rol)
            => await userManager.GetUsersInRoleAsync(rol);

    }
}
