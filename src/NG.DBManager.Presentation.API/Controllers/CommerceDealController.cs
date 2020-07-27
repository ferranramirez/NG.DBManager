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
    public class CommerceDealController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public CommerceDealController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get CommerceDeal
        /// </summary>
        [HttpGet("{AudioDealId}")]
        public IActionResult Get(Guid CommerceId, Guid DealId)
        {
            var CommerceDeal = _uow.Repository<CommerceDeal>()
                .Find(ad => ad.CommerceId == CommerceId && ad.DealId == DealId);
            return Ok(CommerceDeal);
        }

        /// <summary>
        /// Get All CommerceDeals
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<CommerceDeal>().GetAll());
        }

        /// <summary>
        /// Add CommerceDeal
        /// </summary>
        [HttpPost()]
        public IActionResult Add(CommerceDeal CommerceDeal)
        {
            _uow.Repository<CommerceDeal>().Add(CommerceDeal);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update CommerceDeal
        /// </summary>
        [HttpPut]
        public IActionResult Update(CommerceDeal CommerceDeal)
        {
            _uow.Repository<CommerceDeal>().Update(CommerceDeal);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove CommerceDeal
        /// </summary>
        [HttpDelete("{AudioDealId}")]
        public IActionResult Remove(Guid CommerceId, Guid DealId)
        {
            var CommerceDeal = _uow.Repository<CommerceDeal>()
                .Find(ad => ad.CommerceId == CommerceId && ad.DealId == DealId);
            _uow.Repository<CommerceDeal>().Remove(CommerceDeal);
            return Ok(_uow.Commit());
        }
    }
}
