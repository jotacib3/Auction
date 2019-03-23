using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthRepo;
using Contracts;
using ContractsDB;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IRepositoryWrapper repoWrapper,
                              IUnitOfWork unitOfWork, IAuthRepository authRepository)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
            _authRepository = authRepository;
        }

        public IActionResult GetUsers(int page = 1, int size = 10, string roleName = null)
        {
            int skip = (page - 1) * size;
            int total = 0;
            IQueryable<User> query;

            if (roleName != null)
            {
                var role = db.Roles.Where(r => r.Name.Equals(roleName)).First();
                total = db.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).Count();
                query = db.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            }
            else
            {
                total = db.Users.Count();
                query = db.Users;
            }

            var users = query
                .OrderByDescending(c => c.Id)
                .Skip(skip)
                .Take(size)
                .ToList();

            return Ok(new PagedResult<User>(users, page, size, total));
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var vehicles = db.Vehicles.Where(v => v.User.Id.Equals(user.Id));
            db.Vehicles.RemoveRange(vehicles);

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        [Route("api/Users/AddRole")]
        [ResponseType(typeof(User))]
        public IHttpActionResult AddRole(string id, string roleName)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var roleresult = UserManager.AddToRole(user.Id, roleName);

            db.SaveChanges();

            return Ok(user);
        }

        [Route("api/Users/RemoveRole")]
        [ResponseType(typeof(User))]
        public IHttpActionResult RemoveRole(string id, string roleName)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var roleresult = UserManager.RemoveFromRole(user.Id, roleName);

            db.SaveChanges();

            return Ok(user);
        }

        [Route("api/Users/Authorize")]
        [ResponseType(typeof(User))]
        public IHttpActionResult AuthorizeUser(string id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Enabled = true;

            db.SaveChanges();

            return Ok(user);
        }

        [Route("api/Users/Unauthorize")]
        [ResponseType(typeof(User))]
        public IHttpActionResult UnauthorizeUser(string id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Enabled = false;

            db.SaveChanges();

            return Ok(user);
        }
    }
}