using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IAPIUnitOfWork _uow;
        private readonly NgContext _context;

        public TestController(ILogger<TestController> logger,
            IAPIUnitOfWork uow, NgContext context)
        {
            _logger = logger;
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// This is a test Test 
        /// </summary>
        [HttpGet]

        [ProducesResponseType(typeof(List<Tour>), 200)]
        public async Task<IActionResult> Get()
        {
            var tours = await _uow.Tour.GetFeatured();

            return Ok(tours);
        }

        [HttpGet("GetByTag/{Filter}")]
        [ProducesResponseType(typeof(List<Tour>), 200)]
        public IActionResult GetByTag(string filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _uow.Tour.GetByTag(filter);

            return Ok(response);
        }

        [HttpGet("Seed")]
        [ProducesResponseType(typeof(List<Tour>), 200)]
        public IActionResult Seed()
        {
            var dbUtilities = new DatabaseUtilities();
            var rows = dbUtilities.Seed(_context);

            return Ok(rows);
        }
    }
}
