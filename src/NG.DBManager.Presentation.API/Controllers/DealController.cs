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
    public class DealController : ControllerBase
    {
        private readonly IAPIUnitOfWork _uow;
        private readonly NgContext _context;

        public DealController(IAPIUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Deal
        /// </summary>
        [HttpGet("{DealId}")]
        public IActionResult Get(Guid DealId)
        {
            var Deal = _uow.Deal.Get(DealId);
            return Ok(Deal);
        }

        /// <summary>
        /// Get All Deals
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Deal>().GetAll(d => d.DealType));
        }

        /// <summary>
        /// Add Deal
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Deal Deal)
        {
            _uow.Repository<Deal>().Add(Deal);
            _uow.Commit();
            return Ok(Deal.Id);
        }

        /// <summary>
        /// Update Deal
        /// </summary>
        [HttpPut]
        public IActionResult Update(Deal Deal)
        {
            _uow.Repository<Deal>().Update(Deal);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Deal
        /// </summary>
        [HttpDelete("{DealId}")]
        public IActionResult Remove(Guid DealId)
        {
            _uow.Repository<Deal>().Remove(DealId);
            return Ok(_uow.Commit());
        }
    }
}
