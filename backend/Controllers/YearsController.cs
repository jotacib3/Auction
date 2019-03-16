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
    public class YearsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public YearsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYear()
        {
            var years = await _repoWrapper.Year.FindAll();
            return Ok(years);
        }

        [HttpGet("{id}", Name = "YearById")]
        public async Task<IActionResult> GetTransmissionById(int id)
        {

            var year = await _repoWrapper.Year.GetById(id);
            if (year.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(year);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Createtransmission([FromBody] Year year)
        {
            if (year.IsObjectNull())
            {
                return BadRequest("Year object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Year.Create(year);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("YearById", new { id = year.Id }, year);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransmission([FromBody] Year year, int id)
        {
            if (year.IsObjectNull())
            {
                return BadRequest("Year object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (year.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Year.Update(year);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYear(int id)
        {
            var year = await _repoWrapper.Year.GetById(id);

            if (year.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Year.Delete(year);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}