using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        private readonly DbContext _context;

        public CouponRepository(DbContext context) : base(context)
        {
            _context = context;
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
                            .FirstOrDefault();

            return commerce;
        }
        public override Coupon Get(object id)
        {
            return DbSet
                .Where(c => c.Id == (Guid)id)
                .Include(c => c.User)
                .Include(c => c.Node)
                .SingleOrDefault();
        }

        public int InvalidatePastCoupons(Guid userId, Guid nodeId)
        {
            var pastCoupons = DbSet.Where(c => c.UserId == userId && c.NodeId == nodeId && c.ValidationDate == default).ToList();

            pastCoupons.ForEach(pc => pc.ValidationDate = pc.GenerationDate);

            DbSet.UpdateRange(pastCoupons);

            return pastCoupons.Count;
        }

        public async Task<Coupon> GetLastByNode(Guid userId, Guid nodeId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(c => c.UserId == userId &&
                    c.NodeId == nodeId)
                .OrderBy(c => c.ValidationDate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CouponInfo>> GetByCommerce(Guid CommerceId)
        {
            var commerce = _context.Set<Commerce>()
                .SingleOrDefault(com => com.Id == CommerceId);

            if (commerce == null) return null;

            var couponInfo = await DbSet
                .Where(c => c.Node.LocationId == commerce.LocationId)
                .Include(c => c.Node)
                    .ThenInclude(n => n.Tour)
                .Include(c => c.User)
                .Select(c => new CouponInfo
                {
                    CouponId = c.Id,
                    TourInfo = new TourInfo { Id = c.Node.TourId, Name = c.Node.Tour.Name },
                    ValidationDate = c.ValidationDate,
                    DealType = c.Node.Deal.DealType,
                    UserName = c.User.Name,
                    IsSelfValidated = c.IsSelfValidated,
                })
                .ToListAsync();

            return couponInfo;
        }
    }
}
