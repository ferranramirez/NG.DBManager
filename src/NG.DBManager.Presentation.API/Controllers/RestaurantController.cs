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
    public class RestaurantController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public RestaurantController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Restaurant
        /// </summary>
        [HttpGet("{RestaurantId}")]
        public IActionResult Get(Guid RestaurantId)
        {
            var Node = _uow.Repository<Restaurant>().Get(RestaurantId);
            return Ok(Node);
        }

        /// <summary>
        /// Get All Restaurants
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Restaurant>().GetAll());
        }

        /// <summary>
        /// Add Restaurant
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Restaurant Restaurant)
        {
            _uow.Repository<Restaurant>().Add(Restaurant);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update Restaurant
        /// </summary>
        [HttpPut]
        public IActionResult Update(Restaurant Restaurant)
        {
            _uow.Repository<Restaurant>().Update(Restaurant);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Restaurant
        /// </summary>
        [HttpDelete("{RestaurantId}")]
        public IActionResult Remove(Guid RestaurantId)
        {
            _uow.Repository<Restaurant>().Remove(RestaurantId);
            return Ok(_uow.Commit());
        }
    }
}
