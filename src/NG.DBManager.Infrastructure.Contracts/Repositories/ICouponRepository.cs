using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        int InvalidatePastCoupons(Guid userId, Guid nodeId);
        Task<Coupon> GetLastByNode(Guid userId, Guid nodeId);
        Task<IEnumerable<CouponInfo>> GetByCommerce(Guid CommerceId);
    }
}
