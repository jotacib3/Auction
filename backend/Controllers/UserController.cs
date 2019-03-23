using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthRepo;
using backend.Helpers;
using Contracts;
using ContractsDB;
using Entities.Helpers;
using Entities.Models;
using Entities.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet("inicializar")]
        public async Task<IActionResult> Inicialzar()
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole(){Id="1", Name = UserParams.ROLE_EMPLOYEE},
                new IdentityRole(){Id="2", Name = UserParams.ROLE_DISTRIBUTOR},
                new IdentityRole(){Id="3", Name = UserParams.ROLE_ADMIN},
                new IdentityRole(){Id="4", Name = UserParams.ROLE_ADMINAMDGM}
            };
            foreach (var rol in roles)
                if (await _repoWrapper.Role.GetById(rol.Id) == null)
                    _repoWrapper.Role.Create(rol);

            await _unitOfWork.SaveChangesAsync();

            List<RegisterModel> models = new List<RegisterModel>()
            {
                new RegisterModel(){ Email= "Melisa@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Rosa@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "digna@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Jotica@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Oscar@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "harold@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },
                new RegisterModel(){ Email= "Tiny@estudiantes.matcom.uh.cu",
                                    Password = "Jotica123@123",
                                    ConfirmPassword = "Jotica123@123",
                                    Role = "EMPLOYEE"
                },

            };

            foreach (var model in models)
                await _authRepository.Register(model);

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams param)
        {
            IQueryable<User> query;
            if (param.Role != null)
            {
                var role = await _repoWrapper.Role.FindByCondition(r=>r.Name.Equals(param.Role));
                if (role != null)
                    query = await _repoWrapper.User.FindAllInRole(role.Id);
                else
                    query = _repoWrapper.User.UsersQueryable(); 
            }
            else
            {
                query = _repoWrapper.User.UsersQueryable();
            }

            var users = await PagedList<User>.CreateAsync(query.OrderByDescending(
                      c => c.Id), param.PageNumber, param.PageSize);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users.ToList());           
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var publications = await _repoWrapper.Publication.FindByCondition(v => v.User.Id.Equals(user.Id));
            foreach (var publ in publications)
                _repoWrapper.Publication.Delete(publ);

            _repoWrapper.User.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(user);
        }

        

        [HttpPost("user/authorize/{id}")]
        public async Task<IActionResult> AuthorizeUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Enabled = true;

            _repoWrapper.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("users/unauthorize/{id}")]
        public async Task<IActionResult> UnAuthorizeUser(string id)
        {
            var user = await _repoWrapper.User.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            _repoWrapper.User.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(user);
        }
    }
}