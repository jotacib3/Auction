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
    public class PublicationsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public PublicationsController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublucations()
        {
            var publications = await _repoWrapper.Publication.FindAll();
            return Ok(publications);
        }

        [HttpGet("{id}", Name = "PublicationById")]
        public async Task<IActionResult> GetPublicationById(int id)
        {

            var publication = await _repoWrapper.Publication.GetById(id);
            if (publication.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(publication);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublication([FromBody] Publication publication)
        {
            if (publication.IsObjectNull())
            {
                return BadRequest("Publication object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }
            publication.Created = DateTime.Now;
            publication.Updated = DateTime.Now;
            publication.Enabled = null;

            _repoWrapper.Publication.Create(publication);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtRoute("PublicationById", new { id = publication.Id }, publication);
        }

        [HttpPut("Authorize/{id}")]
        public async Task<IActionResult> Authorize(int id)
        {
            var publication = await _repoWrapper.Publication.GetById(id);
            if(publication.IsObjectNull())
            {
                return NotFound();
            }

            publication.Enabled = true;
            _repoWrapper.Publication.Update(publication);
            await _unitOfWork.SaveChangesAsync();

            return Ok(publication);

        }

        [HttpPut("Unauthorize/{id}")]
        public async Task<IActionResult> Unauthorize(int id)
        {
            var publication = await _repoWrapper.Publication.GetById(id);
            if (publication.IsObjectNull())
            {
                return NotFound();
            }

            publication.Enabled = false;
            _repoWrapper.Publication.Update(publication);
            await _unitOfWork.SaveChangesAsync();

            return Ok(publication);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublication([FromBody] Publication publication, int id)
        {
            if (publication.IsObjectNull())
            {
                return BadRequest("Publication object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (publication.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Publication.Update(publication);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(int id)
        {
            var publication = await _repoWrapper.Publication.GetById(id);

            if (publication.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Publication.Delete(publication);
           await  _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}