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
    public class DoorsNumbersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public DoorsNumbersController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoors()
        {
            var doors = await _repoWrapper.DoorsNumber.FindAll();
            return Ok(doors);
        }

        [HttpGet("{id}", Name = "DoorById")]
        public async Task<IActionResult> GetDoorById(int id)
        {

            var door = await _repoWrapper.DoorsNumber.GetById(id);
            if (door.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(door);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoorNumber([FromBody] DoorsNumber door)
        {
            if (door.IsObjectNull())
            {
                return BadRequest("door object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.DoorsNumber.Create(door);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("DoorById", new { id = door.Id }, door);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoorNumber([FromBody] DoorsNumber door, int id)
        {
            if (door.IsObjectNull())
            {
                return BadRequest("Door object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (door.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.DoorsNumber.Update(door);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoorNumber(int id)
        {
            var door = await _repoWrapper.DoorsNumber.GetById(id);

            if (door.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.DoorsNumber.Delete(door);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}