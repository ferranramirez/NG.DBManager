using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        public CouponRepository(DbContext context) : base(context) { }

        public override Coupon Get(object id)
        {
            return DbSet
                .Where(c => c.Id == (Guid)id)
                .Include(c => c.User)
                .Include(c => c.Node)
                .SingleOrDefault();
        }
    }
}
