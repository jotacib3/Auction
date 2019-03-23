using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using Contracts;
using ContractsDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public RolesController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _repoWrapper.Role.FindAll();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {

            var roles = new List<IdentityRole>
            {
                new IdentityRole(){Id="1", Name = Params.ROLE_EMPLOYEE},
                new IdentityRole(){Id="2", Name = Params.ROLE_DISTRIBUTOR},
                new IdentityRole(){Id="3", Name = Params.ROLE_ADMIN},
                new IdentityRole(){Id="4", Name = Params.ROLE_ADMINAMDGM}
            };
            foreach (var rol in roles)
                if(await _repoWrapper.Role.GetById(rol.Id) == null)
                     _repoWrapper.Role.Create(rol);

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}