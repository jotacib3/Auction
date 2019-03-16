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
    public class FuelsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public FuelsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllfuels()
        {
            var fuels = await _repoWrapper.Fuel.FindAll();
            return Ok(fuels);
        }

        [HttpGet("{id}", Name = "FuelById")]
        public async Task<IActionResult> GetFuelById(int id)
        {

            var fuel = await _repoWrapper.Fuel.GetById(id);
            if (fuel.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(fuel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFuel([FromBody] Fuel fuel)
        {
            if (fuel.IsObjectNull())
            {
                return BadRequest("Fuel object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Fuel.Create(fuel);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("FuelById", new { id = fuel.Id }, fuel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuel([FromBody] Fuel fuel, int id)
        {
            if (fuel.IsObjectNull())
            {
                return BadRequest("Fuel object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (fuel.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Fuel.Update(fuel);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuel(int id)
        {
            var fuel = await _repoWrapper.Fuel.GetById(id);

            if (fuel.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Fuel.Delete(fuel);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}