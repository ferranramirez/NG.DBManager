using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly IAPIUnitOfWork _uow;

        public CouponController(IAPIUnitOfWork uow)
        {
            _uow = uow;
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
        public async Task<IActionResult> GetAll()
        {
            var coupon = await _uow.Repository<Coupon>().GetAll();
            return Ok(coupon);
        }

        /// <summary>
        /// Get All Coupons By Commerce
        /// </summary>
        [HttpGet("ByCommerce/{CommerceId}")]
        public async Task<IActionResult> GetByCommerce(Guid CommerceId)
        {
            var coupons = await _uow.Coupon.GetByCommerce(CommerceId);
            return Ok(coupons);
        }

        /// <summary>
        /// Add Coupon
        /// </summary>
        [HttpPost()]
        public IActionResult Add(Coupon Coupon)
        {
            _uow.Repository<Coupon>().Add(Coupon);
            _uow.Commit();
            return Ok(Coupon.Id);
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

        /// <summary>
        /// Remove User's Coupons
        /// </summary>
        [HttpDelete("ByUser/{UserId}")]
        public IActionResult RemoveUserCoupons(Guid UserId)
        {
            var coupons = _uow.Repository<Coupon>()
                .Find(c => c.UserId == UserId);
            _uow.Repository<Coupon>().RemoveRange(coupons);
            return Ok(_uow.Commit());
        }

    }
}
