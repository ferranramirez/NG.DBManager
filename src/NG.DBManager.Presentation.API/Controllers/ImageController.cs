using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public ImageController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Image
        /// </summary>
        [HttpGet("{ImageId}")]
        public IActionResult Get(Guid ImageId)
        {
            var Node = _uow.Repository<Image>().Get(ImageId);
            return Ok(Node);
        }

        /// <summary>
        /// Get All Images
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Image>().GetAll());
        }

        /// <summary>
        /// Add Image
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Image Image)
        {
            _uow.Repository<Image>().Add(Image);
            _uow.Commit();
            return Ok(Image.Id);
        }

        /// <summary>
        /// Update Image
        /// </summary>
        [HttpPut]
        public IActionResult Update(Image Image)
        {
            _uow.Repository<Image>().Update(Image);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Image
        /// </summary>
        [HttpDelete("{ImageId}")]
        public IActionResult Remove(Guid ImageId)
        {
            _uow.Repository<Image>().Remove(ImageId);
            return Ok(_uow.Commit());
        }
    }
}
