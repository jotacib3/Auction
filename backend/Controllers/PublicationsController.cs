using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using Contracts;
using ContractsDB;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;
using Entities.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> GetPublications([FromQuery]PagedParams param)
        {
            var queryable = _repoWrapper.Publication.Queryable()
                                        .Include(p => p.Photos)
                                        .Include(p => p.Brand)
                                        .Include(p => p.Fuel);
            
                                  
            var publications = await PagedList<Publication>.CreateAsync(queryable.OrderByDescending(
                      c => c.Id), param.PageNumber, param.PageSize);

            Response.AddPagination(publications.CurrentPage, publications.PageSize,
                                   publications.TotalCount, publications.TotalPages);



            return Ok(publications.ToList());
        }

        // POST: api/Vehicles/Filter
        [HttpPost("Filter")]
        public async Task<IActionResult> FilterPublications(PublicationSearchModel filters, [FromQuery]PagedParams param)
        {
            var queryable = SearchPublication(filters);
            var publications = await PagedList<Publication>.CreateAsync(queryable.OrderByDescending(
                      c => c.Id), param.PageNumber, param.PageSize);

            Response.AddPagination(publications.CurrentPage, publications.PageSize,
                                   publications.TotalCount, publications.TotalPages);

            return Ok(publications.ToList());
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
        public async Task<IActionResult> CreatePublication( [FromBody]Publication publication )
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

        private IQueryable<Publication> SearchPublication(PublicationSearchModel filters = null)
        {
            var query = _repoWrapper.Publication.Queryable();

            if (filters.UserId != null)
                query = _repoWrapper.Publication.FindQueryable(v => v.UserId == filters.UserId);

            if (filters.InvoiceNumber != null)
                query = query.Where(v => v.InvoiceNumber == filters.InvoiceNumber);

            if (filters.MinMileage != null)
                query = query.Where(v => v.Mileage >= filters.MinMileage);

            if (filters.MaxMileage != null)
                query = query.Where(v => v.Mileage >= filters.MaxMileage);

            if (filters.SerialNumber != null)
                query = query.Where(v => v.SerialNumber.StartsWith(filters.SerialNumber));

            if (filters.InsideColor != null)
                query = query.Where(v => v.InsideColor.StartsWith(filters.InsideColor));

            if (filters.OutsideColor != null)
                query = query.Where(v => v.OutsideColor.StartsWith(filters.OutsideColor));

            if (filters.MinPrice != null)
                query = query.Where(v => v.Price >= filters.MinPrice);

            if (filters.MaxPrice != null)
                query = query.Where(v => v.Price <= filters.MaxPrice);

            if (filters.BrandId != null)
                query = query.Where(v => v.BrandId == filters.BrandId);

            if (filters.ModelId != null)
                query = query.Where(v => v.ModelId == filters.ModelId);

            if (filters.FuelId != null)
                query = query.Where(v => v.FuelId == filters.FuelId);

            if (filters.LocationId != null)
                query = query.Where(v => v.LocationId == filters.LocationId);

            if (filters.PackId != null)
                query = query.Where(v => v.PackId == filters.PackId);

            if (filters.TransmissionId != null)
                query = query.Where(v => v.TransmissionId == filters.TransmissionId);

            if (filters.VersionId != null)
                query = query.Where(v => v.VersionId == filters.VersionId);

            if (filters.YearId != null)
                query = query.Where(v => v.YearId == filters.YearId);

            if (filters.DoorsNumberId != null)
                query = query.Where(v => v.DoorsNumberId == filters.DoorsNumberId);

            if (filters.UserId != null)
                query = query.Where(v => v.UserId == filters.UserId);

            return query;
        }
    }
}