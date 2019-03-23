using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using ContractsDB;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacksController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;

        public PacksController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPacks()
        {
            var packs = await _repoWrapper.Pack.FindAll();
            return Ok(packs);
        }

        [HttpGet("{id}", Name = "PackById")]
        public async Task<IActionResult> GetPackById(int id)
        {

            var pack = await _repoWrapper.Pack.GetById(id);
            if (pack.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(pack);
            }
        }

        [HttpPost]
        public IActionResult CreatePack([FromBody] Pack pack)
        {
            if (pack.IsObjectNull())
            {
                return BadRequest("Pack object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Pack.Create(pack);
            _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("PackById", new { id = pack.Id }, pack);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackn([FromBody] Pack pack, int id)
        {
            if (pack.IsObjectNull())
            {
                return BadRequest("Pack object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (pack.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Pack.Update(pack);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePack(int id)
        {
            var pack = await _repoWrapper.Pack.GetById(id);

            if (pack.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Pack.Delete(pack);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}