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
            var user = _uow.Repository<User>().Get(UserId);
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
        public IActionResult Add(User user)
        {
            _uow.User.Add(user);
            _uow.Commit();
            return Ok(user.Id);
        }

        /// <summary>
        /// Update User
        /// </summary>
        [HttpPut]
        public IActionResult Update(User user)
        {
            _uow.Repository<User>().Update(user);
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
