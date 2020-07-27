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
    public class TagController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public TagController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Tag
        /// </summary>
        [HttpGet("{TagId}")]
        public IActionResult Get(Guid TagId)
        {
            var Node = _uow.Repository<Tag>().Get(TagId);
            return Ok(Node);
        }

        /// <summary>
        /// Get All Tags
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Tag>().GetAll());
        }

        /// <summary>
        /// Add Tag
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Tag Tag)
        {
            _uow.Repository<Tag>().Add(Tag);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update Tag
        /// </summary>
        [HttpPut]
        public IActionResult Update(Tag Tag)
        {
            _uow.Repository<Tag>().Update(Tag);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Tag
        /// </summary>
        [HttpDelete("{TagId}")]
        public IActionResult Remove(Guid TagId)
        {
            _uow.Repository<Tag>().Remove(TagId);
            return Ok(_uow.Commit());
        }
    }
}
