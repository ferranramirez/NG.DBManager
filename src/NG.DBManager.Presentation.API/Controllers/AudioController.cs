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
    public class AudioController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public AudioController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Audio
        /// </summary>
        [HttpGet("{AudioId}")]
        public IActionResult Get(Guid AudioId)
        {
            var Node = _uow.Repository<Audio>().Get(AudioId);
            return Ok(Node);
        }

        /// <summary>
        /// Get All Audios
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Audio>().GetAll());
        }

        /// <summary>
        /// Add Audio
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Audio Audio)
        {
            _uow.Repository<Audio>().Add(Audio);
            _uow.Commit();
            return Ok(Audio.Id);
        }

        /// <summary>
        /// Update Audio
        /// </summary>
        [HttpPut]
        public IActionResult Update(Audio Audio)
        {
            _uow.Repository<Audio>().Update(Audio);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Audio
        /// </summary>
        [HttpDelete("{AudioId}")]
        public IActionResult Remove(Guid AudioId)
        {
            _uow.Repository<Audio>().Remove(AudioId);
            return Ok(_uow.Commit());
        }
    }
}
