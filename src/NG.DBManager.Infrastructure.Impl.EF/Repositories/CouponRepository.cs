using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class CouponRepository : Repository<Coupon>, IRepository
    {
        public CouponRepository(DbContext context) : base(context) { }

        public override void Add(Coupon entity)
        {
            DbSet.Add(entity);
            Context.Entry(entity).Property("Created").CurrentValue = DateTime.UtcNow;
        }

        public override Coupon Get(object id)
        {
            return DbSet.Find(id);
        }
    }
}
