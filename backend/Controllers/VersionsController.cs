using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using ContractsDB;
using Entities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;

        public VersionsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVersion()
        {
            var versions = await _repoWrapper.Version.FindAll();
            return Ok(versions);
        }

        [HttpGet("{id}", Name = "VersionById")]
        public async Task<IActionResult> GetVersionById(int id)
        {

            var version = await _repoWrapper.Version.GetById(id);
            if (version.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(version);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] Entities.Models.Version version)
        {
            if (version.IsObjectNull())
            {
                return BadRequest("Version object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Version.Create(version);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("VersionById", new { id = version.Id }, version);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVerion([FromBody] Entities.Models.Version version, int id)
        {
            if (version.IsObjectNull())
            {
                return BadRequest("Version object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (version.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Version.Update(version);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            var version = await _repoWrapper.Version.GetById(id);

            if (version.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Version.Delete(version);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}