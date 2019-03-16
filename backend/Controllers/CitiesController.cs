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
    public class CitiesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public CitiesController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _repoWrapper.City.FindAll();
            return Ok(cities);
        }

        [HttpGet("{id}", Name = "CityById")]
        public async Task<IActionResult> GetCityById(int id)
        {

            var city = await _repoWrapper.City.GetById(id);
            if (city.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(city);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (city.IsObjectNull())
            {
                return BadRequest("City object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.City.Create(city);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("CityById", new { id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity([FromBody] City city, int id)
        {
            if (city.IsObjectNull())
            {
                return BadRequest("City object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (city.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.City.Update(city);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _repoWrapper.City.GetById(id);

            if (city.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.City.Delete(city);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}