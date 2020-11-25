using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Commerce GetCommerce(Guid couponId);
        int InvalidatePastCoupons(Guid userId, Guid nodeId);
        Task<Coupon> GetLastByNode(Guid userId, Guid nodeId);
    }
}
