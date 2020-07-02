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
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _host;

        private readonly int MAX_BYTES = 3 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES =new[] {".jpg","jpeg", ".png" };
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
        public async Task<IActionResult> CreatePhotosPublication(int id, List<IFormFile> files)
        { 
            if (files.Count != 5)
            {
                return BadRequest("Is not 5 photos");
            }
             
            var publication = await _repoWrapper.Publication.Queryable()
                                    .Include(a => a.Photos).Where(p => p.Id ==id).FirstOrDefaultAsync();

            if (publication.Id != id)
                return NotFound();

            foreach (var file in files)
            {
                if (file == null)
                {
                    return BadRequest("Photos objects is null");
                }
                if (file.Length == 0)
                {
                    return BadRequest("Empty file");
                }

                if (file.Length > MAX_BYTES)
                {
                    return BadRequest("Max file size exceeded");
                }
                if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName)))
                {
                    return BadRequest("Invalid file type.");
                }
                var Photo = await Upload(file);
                if (publication.Photos == null)
                    publication.Photos.Add(Photo);
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("employee/{id}")]
        public async Task<IActionResult> CreatePhotosEmployee(string id, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("Photos objects is null");
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty file");
            }

            if(file.Length > MAX_BYTES)
            {
                return BadRequest("Max file size exceeded");
            }
            if(!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName)))
            {
                return BadRequest("Invalid file type.");
            }
            

            var employee = await _repoWrapper.Employee.FindQueryable(p => p.UserId.Equals(id)).FirstOrDefaultAsync();

            if (!employee.UserId.Equals(id))
                return NotFound();

            var Photo = await Upload(file);
            employee.Photo = Photo;

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto([FromBody] Photo photo, int id)
        {
            if (photo.IsObjectNull())
            {
                return BadRequest("Brand object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object Model");
            }

            if (photo.IsDifferentObject(id))
            {
                return NotFound();
            }

            _repoWrapper.Photo.Update(photo);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo= await _repoWrapper.Photo.GetById(id);

            if (photo.IsObjectNull())
            {
                return NotFound();
            }
            _repoWrapper.Photo.Delete(photo);
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