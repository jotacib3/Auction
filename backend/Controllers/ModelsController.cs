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
    public class ModelsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public ModelsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            var models = await _repoWrapper.Model.FindAll();
            return Ok(models);
        }

        [HttpGet("{id}", Name = "ModelById")]
        public async Task<IActionResult> GetMModelById(int id)
        {

            var model = await _repoWrapper.Model.GetById(id);
            if (model.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] Model model)
        {
            if (model.IsObjectNull())
            {
                return BadRequest("Model object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Model.Create(model);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("ModelById", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel([FromBody] Model model, int id)
        {
            if (model.IsObjectNull())
            {
                return BadRequest("Model object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (model.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Model.Update(model);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _repoWrapper.Model.GetById(id);

            if (model.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Model.Delete(model);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}