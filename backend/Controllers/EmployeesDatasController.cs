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
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesDatasController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesDatasController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employee = await _repoWrapper.Employee.FindAll();
            return Ok(employee);
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {

            var employee = await _repoWrapper.Employee.GetById(id);
            if (employee.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeNumber([FromBody] EmployeeData employee)
        {
            if (employee.IsObjectNull())
            {
                return BadRequest("Employee object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            _repoWrapper.Employee.Create(employee);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("EmployeeById", new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeNumber([FromBody] EmployeeData employee, int id)
        {
            if (employee.IsObjectNull())
            {
                return BadRequest("Employee object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (employee.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Employee.Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _repoWrapper.Employee.GetById(id);

            if (employee.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Employee.Delete(employee);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}