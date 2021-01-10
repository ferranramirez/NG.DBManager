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
    public class NodeController : ControllerBase
    {
        private readonly IAPIUnitOfWork _uow;
        private readonly NgContext _context;

        public NodeController(IAPIUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Node
        /// </summary>
        [HttpGet("{NodeId}")]
        public IActionResult Get(Guid NodeId)
        {
            var Node = _uow.Node.Get(NodeId);
            return Ok(Node);
        }

        /// <summary>
        /// Get All Nodes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<Node>()
                        .GetAll(n => n.Deal));
        }

        /// <summary>
        /// Add Node
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Node Node)
        {
            _uow.Repository<Node>().Add(Node);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update Node
        /// </summary>
        [HttpPut]
        public IActionResult Update(Node Node)
        {
            _uow.Repository<Node>().Update(Node);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Node
        /// </summary>
        [HttpDelete("{NodeId}")]
        public IActionResult Remove(Guid NodeId)
        {
            _uow.Repository<Node>().Remove(NodeId);
            return Ok(_uow.Commit());
        }
    }
}
