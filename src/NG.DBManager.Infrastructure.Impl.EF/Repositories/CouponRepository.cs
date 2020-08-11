using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        private readonly DbContext _context;

        public CouponRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public override Coupon Get(object id)
        {
            return DbSet
                .Where(c => c.Id == (Guid)id)
                .Include(c => c.User)
                .Include(c => c.Node)
                .SingleOrDefault();
        }

        public Commerce GetCommerce(Guid couponId)
        {
            var commerceSet = _context.Set<Commerce>();
            var couponLocationId = DbSet
                        .Where(c => c.Id == couponId)
                        // .Include(c => c.Node)
                        .Select(x => x.Node.LocationId)
                        .SingleOrDefault();

            var commerce = commerceSet
                            .Where(com => com.LocationId == couponLocationId)
                            .SingleOrDefault();
            return commerce;
        }
        public int InvalidatePastCoupons(Guid userId, Guid nodeId)
        {
            var pastCoupons = DbSet.Where(c => c.UserId == userId && c.NodeId == nodeId && !c.IsValidated).ToList();

            pastCoupons.ForEach(pc => pc.ValidationDate = pc.GenerationDate);

            DbSet.UpdateRange(pastCoupons);

            return pastCoupons.Count;
        }
    }
}
