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
    public class LocationController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public LocationController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Location
        /// </summary>
        [HttpGet("{LocationId}")]
        public IActionResult Get(Guid LocationId)
        {
            var Node = _uow.Repository<Location>().Get(LocationId);
            return Ok(Node);
        }

        /// <summary>
        /// Get All Locations
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Location>().GetAll());
        }

        /// <summary>
        /// Add Location
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Location Location)
        {
            _uow.Repository<Location>().Add(Location);
            _uow.Commit();
            return Ok(Location.Id);
        }

        /// <summary>
        /// Update Location
        /// </summary>
        [HttpPut]
        public IActionResult Update(Location Location)
        {
            _uow.Repository<Location>().Update(Location);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Location
        /// </summary>
        [HttpDelete("{LocationId}")]
        public IActionResult Remove(Guid LocationId)
        {
            _uow.Repository<Location>().Remove(LocationId);
            return Ok(_uow.Commit());
        }
    }
}
