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
    public class DealTypeController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public DealTypeController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get DealType
        /// </summary>
        [HttpGet("{DealTypeId}")]
        public IActionResult Get(Guid DealTypeId)
        {
            var Deal = _uow.Repository<DealType>().Get(DealTypeId);
            return Ok(Deal);
        }

        /// <summary>
        /// Get All DealTypes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<DealType>().GetAll());
        }

        /// <summary>
        /// Add DealType
        /// </summary>
        [HttpPost()]
        public IActionResult Add(DealType DealType)
        {
            _uow.Repository<DealType>().Add(DealType);
            _uow.Commit();
            return Ok(DealType.Id);
        }

        /// <summary>
        /// Update DealType
        /// </summary>
        [HttpPut]
        public IActionResult Update(DealType DealType)
        {
            _uow.Repository<DealType>().Update(DealType);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove DealType
        /// </summary>
        [HttpDelete("{DealTypeId}")]
        public IActionResult Remove(Guid DealTypeId)
        {
            _uow.Repository<DealType>().Remove(DealTypeId);
            return Ok(_uow.Commit());
        }
    }
}
