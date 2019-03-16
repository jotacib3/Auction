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
    public class TransmissionsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public TransmissionsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransmission()
        {
            var transmissions = await _repoWrapper.Transmission.FindAll();
            return Ok(transmissions);
        }

        [HttpGet("{id}", Name = "TransmissionById")]
        public async Task<IActionResult> GetTransmissionById(int id)
        {

            var transmission = await _repoWrapper.Transmission.GetById(id);
            if (transmission.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(transmission);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Createtransmission([FromBody] Transmission transmission)
        {
            if (transmission.IsObjectNull())
            {
                return BadRequest("Transmission object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Transmission.Create(transmission);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("TransmissionById", new { id = transmission.Id }, transmission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransmission([FromBody] Transmission transmission, int id)
        {
            if (transmission.IsObjectNull())
            {
                return BadRequest("State object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (transmission.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Transmission.Update(transmission);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransmission(int id)
        {
            var transmission = await _repoWrapper.Transmission.GetById(id);

            if (transmission.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Transmission.Delete(transmission);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}