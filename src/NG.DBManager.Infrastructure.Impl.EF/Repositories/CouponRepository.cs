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

        public User GetCommerceUser(Guid NodeId)
        {
            return DbSet
                .Include(c => c.Node)
                    .ThenInclude(n => n.Location)
                    .ThenInclude(l => l.Commerce)
                    .ThenInclude(com => com.User)
                .First(c => c.Node.Id == NodeId)
                .Node.Location.Commerce.User;

        }
    }
}
