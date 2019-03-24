using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using ContractsDB;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _host;
        public PhotoController(IRepositoryWrapper repoWrapper, IUnitOfWork unitOfWork,
                               IHostingEnvironment host)
        {
            _repoWrapper = repoWrapper;
            _unitOfWork = unitOfWork;
            _host = host;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhotos()
        {
            var photos = await _repoWrapper.Photo.FindAll();
            return Ok(photos);
        }

        [HttpGet("{id}", Name = "PhotoById")]
        public async Task<IActionResult> GetPhotoById(int id)
        {

            var photo = await _repoWrapper.Photo.GetById(id);
            if (photo.IsObjectNull())
            {
                return NotFound();
            }
            else
            {
                return Ok(photo);
            }
        }

        [HttpPost("publication/{id}")]
        public async Task<IActionResult> CreatePhotosPublication(int id, [FromBody] List<IFormFile> files)
        {
            if (files == null)
            {
                return BadRequest("Photos objects is null");
            }

            if (files.Count != 5)
            {
                return BadRequest("Is not 5 photos");
            }

            var publication = await _repoWrapper.Publication.GetById(id);

            if (publication == null)
                return NotFound();

            foreach(var file in files)
                publication.Photos.Add(await Upload(file));

            await _unitOfWork.SaveChangesAsync();

            return Ok();
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

        private async Task<Photo> Upload(IFormFile file)
        {
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new Photo { FileName = fileName };
        }
    }
}