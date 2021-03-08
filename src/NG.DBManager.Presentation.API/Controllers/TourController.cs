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
    public class TourController : ControllerBase
    {
        private readonly IAPIUnitOfWork _uow;
        private readonly NgContext _context;

        public TourController(IAPIUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Tour
        /// </summary>
        [HttpGet("{TourId}")]
        public IActionResult Get(Guid TourId)
        {
            var Tour = _uow.Tour.Get(TourId);

            return Ok(Tour);
        }

        /// <summary>
        /// Get All Tours
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Tour>().GetAll());
        }

        /// <summary>
        /// Add Tour
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Tour Tour)
        {
            _uow.Repository<Tour>().Add(Tour);
            _uow.Commit();
            return Ok(Tour.Id);
        }

        /// <summary>
        /// Update Tour
        /// </summary>
        [HttpPut]
        public IActionResult Update(Tour Tour)
        {
            _uow.Repository<Tour>().Update(Tour);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Tour
        /// </summary>
        [HttpDelete("{TourId}")]
        public IActionResult Remove(Guid TourId)
        {
            _uow.Repository<Tour>().Remove(TourId);
            return Ok(_uow.Commit());
        }
    }
}
