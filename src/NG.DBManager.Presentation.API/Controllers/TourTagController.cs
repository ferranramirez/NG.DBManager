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
    public class TourTagController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public TourTagController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get TourTag
        /// </summary>
        [HttpGet("{TourTagId}")]
        public IActionResult Get(Guid TourId, Guid TagId)
        {
            var TourTag = _uow.Repository<TourTag>().Find(tt => tt.TourId == TourId && tt.TagId == TagId);
            return Ok(TourTag);
        }

        /// <summary>
        /// Get All TourTags
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<TourTag>().GetAll());
        }

        /// <summary>
        /// Add TourTag
        /// </summary>
        [HttpPost()]
        public IActionResult Add(TourTag TourTag)
        {
            _uow.Repository<TourTag>().Add(TourTag);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update TourTag
        /// </summary>
        [HttpPut]
        public IActionResult Update(TourTag TourTag)
        {
            _uow.Repository<TourTag>().Update(TourTag);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove TourTag
        /// </summary>
        [HttpDelete("{TourTagId}")]
        public IActionResult Remove(Guid TourId, Guid TagId)
        {
            var TourTag = _uow.Repository<TourTag>().Find(tt => tt.TourId == TourId && tt.TagId == TagId);
            _uow.Repository<TourTag>().Remove(TourTag);
            return Ok(_uow.Commit());
        }
    }
}
