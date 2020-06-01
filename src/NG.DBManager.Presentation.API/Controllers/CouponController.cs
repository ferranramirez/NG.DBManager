using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public CouponController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get Coupon
        /// </summary>
        [HttpGet("{CouponId}")]
        public IActionResult Get(Guid CouponId)
        {
            var coupon = _uow.Repository<Coupon>().Get(CouponId);
            return Ok(coupon);
        }

        /// <summary>
        /// Get All Coupons
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var coupon = _uow.Repository<Coupon>().GetAll();
            return Ok(coupon);
        }

        /// <summary>
        /// Add Coupon
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Coupon coupon)
        {
            _uow.Repository<Coupon>().Add(coupon);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update Coupon
        /// </summary>
        [HttpPut]
        public IActionResult Update(Coupon coupon)
        {
            _uow.Repository<Coupon>().Update(coupon);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove Coupon
        /// </summary>
        [HttpDelete("{CouponId}")]
        public IActionResult Remove(Guid CouponId)
        {
            _uow.Repository<Coupon>().Remove(CouponId);
            return Ok(_uow.Commit());
        }

    }
}
