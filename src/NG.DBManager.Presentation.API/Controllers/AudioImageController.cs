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
    public class AudioImageController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public AudioImageController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get AudioImage
        /// </summary>
        [HttpGet("{AudioId}/{ImageId}")]
        public IActionResult Get(Guid AudioId, Guid ImageId)
        {
            var AudioImage = _uow.Repository<AudioImage>()
                .Find(ai => ai.AudioId == AudioId && ai.ImageId == ImageId);
            return Ok(AudioImage);
        }

        /// <summary>
        /// Get All AudioImages
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<AudioImage>().GetAll());
        }

        /// <summary>
        /// Add AudioImage
        /// </summary>
        [HttpPost()]
        public IActionResult Add(AudioImage AudioImage)
        {
            _uow.Repository<AudioImage>().Add(AudioImage);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove AudioImage
        /// </summary>
        [HttpDelete("{AudioId}/{ImageId}")]
        public IActionResult Remove(Guid AudioId, Guid ImageId)
        {
            var AudioImage = _uow.Repository<AudioImage>()
                .Find(ai => ai.AudioId == AudioId && ai.ImageId == ImageId);
            _uow.Repository<AudioImage>().Remove(AudioImage);
            return Ok(_uow.Commit());
        }
    }
}
