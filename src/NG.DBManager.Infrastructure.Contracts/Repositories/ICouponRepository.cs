using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Commerce GetCommerce(Guid couponId);
    }
}
