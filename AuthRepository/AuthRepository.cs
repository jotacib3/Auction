using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ModelsView;
using Microsoft.AspNetCore.Identity;

namespace AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthRepository(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              RoleManager<IdentityRole> roleManager)
        { 
            this.userManager = userManager;
            this.signInManager = signInManager;

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
            if (await userManager.CheckPasswordAsync(userToVerify, model.Password))
                return null;

            return userToVerify;

        }

        public Task<bool> Register(RegisterModel model)
        {
            throw new NotImplementedException();
        }

       /* public async Task<bool> Registers(RegisterModel model)
        {
           
            /*foreach (var rol in model.Roles)
                await roleManager.CreateAsync(new IdentityRole(rol));*/
           /* var user = new User { UserName = model.Username, Email = model.Email , Avatar = model.Avatar};
            var result = await userManager.CreateAsync(user, model.Password);
            var temp = await userManager.AddToRolesAsync(user, model.Roles);
            return result.Succeeded && temp.Succeeded;
        } */

        public async Task<bool> UserExists(string Username)
        {
            return  await userManager.FindByNameAsync(Username) != null; 
        }
    }
}
