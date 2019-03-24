using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Helpers;
using Contracts;
using ContractsDB;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        public OffersController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOffers([FromQuery]PagedParams param, [FromQuery]string Id )
        {
            IQueryable<Offer> queryable = _repoWrapper.Offer.Queryable();

            if (User.IsInRole(UserParams.ROLE_EMPLOYEE))
            {
                queryable.Where(u => u.Publication.UserId.Equals(Id));
            }
            else if (User.IsInRole(UserParams.ROLE_DISTRIBUTOR))
            {
                queryable.Where(d => d.UserId.Equals(Id));
            }
            var offers = await PagedList<Offer>.CreateAsync(queryable.OrderByDescending(
                      c => c.Id), param.PageNumber, param.PageSize);

            Response.AddPagination(offers.CurrentPage, offers.PageSize,
                                   offers.TotalCount, offers.TotalPages);

            return Ok(offers.ToList());
        }

        [HttpGet("{id}", Name = "OfferById")]
        public async Task<IActionResult> GetOfferById(int id)
        {

            var offer = await _repoWrapper.Offer.GetById(id);
            if (offer.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(offer);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] Offer offer)
        {
            if (offer.IsObjectNull())
            {
                return BadRequest("Offer object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }
            offer.Created = DateTime.Now;
            _repoWrapper.Offer.Create(offer);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtRoute("OfferById", new { id = offer.Id }, offer);
        }

        [HttpPut("Accept/{id}")]
        public async Task<IActionResult> Authorize(int id)
        {
            var offer = await _repoWrapper.Offer.GetById(id);
            if (offer.IsObjectNull())
            {
                return NotFound();
            }

            offer.Enabled = true;
            _repoWrapper.Offer.Update(offer);
            await _unitOfWork.SaveChangesAsync();

            return Ok(offer);

        }

        [HttpPut("Unauthorize/{id}")]
        public async Task<IActionResult> Unauthorize(int id)
        {
            var offer = await _repoWrapper.Offer.GetById(id);
            if (offer.IsObjectNull())
            {
                return NotFound();
            }

            offer.Enabled = false;
            _repoWrapper.Offer.Update(offer);
            await _unitOfWork.SaveChangesAsync();

            return Ok(offer);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer([FromBody] Offer offer, int id)
        {
            if (offer.IsObjectNull())
            {
                return BadRequest("Offer object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (offer.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Offer.Update(offer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offer = await _repoWrapper.Offer.GetById(id);

            if (offer.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Offer.Delete(offer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}