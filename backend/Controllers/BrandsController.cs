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
    public class BrandsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public BrandsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _repoWrapper.Brand.FindAll();
            return Ok(brands);
        }

        [HttpGet("{id}", Name = "BrandById")]
        public async Task<IActionResult> GetBrandById(int id)
        {

            var brand = await _repoWrapper.Brand.GetById(id);
            if (brand.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(brand);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            if (brand.IsObjectNull())
            {
                return BadRequest("Brand object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Brand.Create(brand);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("BrandById", new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand([FromBody] Brand brand, int id)
        {
            if (brand.IsObjectNull())
            {
                return BadRequest("Brand object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (brand.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Brand.Update(brand);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _repoWrapper.Brand.GetById(id);

            if (brand.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Brand.Delete(brand);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}