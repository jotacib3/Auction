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
    public class StatesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public StatesController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _repoWrapper.State.FindAll();
            return Ok(states);
        }

        [HttpGet("{id}", Name = "StateById")]
        public async Task<IActionResult> GetStateById(int id)
        {

            var state = await _repoWrapper.State.GetById(id);
            if (state.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(state);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] State state)
        {
            if (state.IsObjectNull())
            {
                return BadRequest("State object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.State.Create(state);
            await _unitOfWork.SaveChangesAsync();


            return CreatedAtRoute("StateById", new { id = state.Id }, state);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublication([FromBody] State state, int id)
        {
            if (state.IsObjectNull())
            {
                return BadRequest("State object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (state.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.State.Update(state);       
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var state = await _repoWrapper.State.GetById(id);

            if (state.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.State.Delete(state);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}