using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAPIUnitOfWork _uow;
        private readonly NgContext _context;

        public UserController(IAPIUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get User
        /// </summary>
        [HttpGet("{UserId}")]
        public IActionResult Get(Guid UserId)
        {
            var user = _uow.User.Get(UserId);
            return Ok(user);
        }

        /// <summary>
        /// Get User
        /// </summary>
        [HttpGet("ByEmail/{UserEmail}")]
        public IActionResult GetByEmail(string UserEmail)
        {
            var user = _uow.Repository<User>()
                .Find(u => u.Email.Equals(UserEmail))
                .SingleOrDefault();

            return Ok(user);
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _uow.Repository<User>().GetAll();
            return Ok(user);
        }

        /// <summary>
        /// Add User
        /// </summary>
        [HttpPost]
        public IActionResult Add(User User)
        {
            _uow.User.Add(User);
            _uow.Commit();
            return Ok(User.Id);
        }

        /// <summary>
        /// Update User
        /// </summary>
        [HttpPut]
        public IActionResult Update(User User)
        {
            _uow.Repository<User>().Update(User);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove User
        /// </summary>
        [HttpDelete("{UserId}")]
        public IActionResult Remove(Guid UserId)
        {
            _uow.Repository<User>().Remove(UserId);
            return Ok(_uow.Commit());
        }

    }
}
