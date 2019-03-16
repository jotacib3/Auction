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
    public class LocationsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public LocationsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _repoWrapper.Location.FindAll();
            return Ok(locations);
        }

        [HttpGet("{id}", Name = "LocationById")]
        public async Task<IActionResult> GetLocationById(int id)
        {

            var location = await _repoWrapper.Location.GetById(id);
            if (location.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(location);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] Location location)
        {
            if (location.IsObjectNull())
            {
                return BadRequest("Location object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Location.Create(location);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("LocationById", new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation([FromBody] Location location, int id)
        {
            if (location.IsObjectNull())
            {
                return BadRequest("Location object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (location.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Location.Update(location);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _repoWrapper.Location.GetById(id);

            if (location.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Location.Delete(location);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}