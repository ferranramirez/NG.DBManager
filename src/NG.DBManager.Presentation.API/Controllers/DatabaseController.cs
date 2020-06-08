using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System.Collections.Generic;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;
        private readonly DatabaseUtilities _dbUtilities;

        public DatabaseController(ILogger<DatabaseController> logger,
            IFullUnitOfWork uow, NgContext context)
        {
            _logger = logger;
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// This is a test Test 
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tour>), 200)]
        public IActionResult Get()
        {
            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("RandomSeed")]
        [ProducesResponseType(typeof(List<Tour>), 200)]
        public IActionResult RandomSeed()
        {
            var rows = _dbUtilities.RandomSeed(_context);

            return Ok(rows);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("UsersSeed")]
        [ProducesResponseType(typeof(List<Tour>), 200)]
        public IActionResult UsersSeed()
        {
            var rows = _dbUtilities.RandomSeed(_context);

            return Ok(rows);
        }

        [HttpGet("Reset")]
        [ProducesResponseType(typeof(List<Tour>), 200)]
        public IActionResult Reset()
        {
            var dbUtilities = new DatabaseUtilities();
            dbUtilities.Reset(_context);

            return Ok();
        }
    }
}
