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
    public class CommerceController : ControllerBase
    {
        private readonly IAPIUnitOfWork _uow;
        private readonly NgContext _context;

        public CommerceController(IAPIUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Commerce
        /// </summary>
        [HttpGet("{CommerceId}")]
        public IActionResult Get(Guid CommerceId) 
        {
            var commerce = _uow.Commerce.Get(CommerceId);
            return Ok(commerce);
        }

        /// <summary>
        /// Get All Commerces
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commerce = await _uow.Repository<Commerce>().GetAll();
            return Ok(commerce);
        }

        /// <summary>
        /// Add Commerce
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Commerce commerce)
        {
            _uow.Repository<Commerce>().Add(commerce);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update Commerce
        /// </summary>
        [HttpPut]
        public IActionResult Update(Commerce commerce)
        {
            _uow.Repository<Commerce>().Update(commerce);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Commerce
        /// </summary>
        [HttpDelete("{CommerceId}")]
        public IActionResult Remove(Guid CommerceId)
        {
            _uow.Repository<Commerce>().Remove(CommerceId);
            return Ok(_uow.Commit());
        }

    }
}
