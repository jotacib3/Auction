﻿using System;
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
        public async Task<IdentityResult> AddRole(IdentityRole role)
        {
            return await roleManager.CreateAsync(role);
        }
        public async Task<IList<string>> GetAllRoles(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<User> Login(LoginModel model)
        {
            var userToVerify = await userManager.FindByEmailAsync(model.Email);
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

            var userDb = await userManager.FindByEmailAsync(model.Email);
            if (model.Role != null && result.Succeeded)
            {
                var roleresult = await userManager.AddToRoleAsync(userDb, model.Role);
            }
            
            return result;
        }

        public async Task<IdentityResult> RegisterEmployee(RegisterEmployeeModel model)
        {
            var user = new User() { UserName = model.UserName,
                                   Email = model.Email, Enabled = null, EmployeeData = model.Employee };

            var result = await userManager.CreateAsync(user, model.Password);

            var userDb = await userManager.FindByEmailAsync(model.Email);
            if (model.Role != null && result.Succeeded)
            {
                var roleresult = await userManager.AddToRoleAsync(userDb, model.Role);
            }
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword,
                model.NewPassword);

            return result;
        }

        public async void ForgotPassword(ForgotPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            string code;
            if (user != null)
             code = await userManager.GeneratePasswordResetTokenAsync(user);
           // await userManager.Se(user, "Reset Password", $"Please reset your password by using this {code}");
        }
        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var result = await userManager.ResetPasswordAsync(user,model.Code,model.Password);

            return result;
        }

        //public async Task<IdentityResult> SetPassword(SetPasswordModel model)
        //{
        //    var user = await userManager.FindByEmailAsync(model.Email);
        //    var result = await userManager.AddPasswordAsync(user, model.NewPassword);

        //    return result;
        //}


        public async Task<User> UserExists(string Username)
        {
            return  await userManager.FindByNameAsync(Username); 
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
