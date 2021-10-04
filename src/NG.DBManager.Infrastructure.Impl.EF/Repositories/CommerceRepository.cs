using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class CommerceRepository : Repository<Commerce>, ICommerceRepository
    {
        private readonly DbContext _context;

        public CommerceRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public override Commerce Get(object id)
        {
            return DbSet
                .Include(c => c.CommerceDeals)
                    .ThenInclude(cd => cd.Deal)
                .SingleOrDefault(c => c.Id == (Guid)id);
        }

        public Commerce GetByCoupon(Guid couponId)
        {
            var couponSet = _context.Set<Coupon>();
            var couponLocationId = couponSet
                        .Where(c => c.Id == couponId)
                        // .Include(c => c.Node)
                        .Select(x => x.Node.LocationId)
                        .SingleOrDefault();

            var commerce = DbSet
                            .Where(com => com.LocationId == couponLocationId)
                            .FirstOrDefault();

            return commerce;
        }

        public override async Task<IEnumerable<Commerce>> GetAll(Expression<Func<Commerce, object>>[] includes)
        {
            return await DbSet
                .Where(c => c.IsActive)
                .ToListAsync();
        }
        public override void Add(Commerce commerce)
        {
            if (commerce == null) { return; }

            LocationUsedException(commerce.LocationId, default);

            DbSet.Add(commerce);
        }

        public override void Update(Commerce commerce)
        {
            if (commerce == null) { return; }

            var oldCommerce = DbSet.Find(commerce.Id);
            LocationUsedException(commerce.LocationId, oldCommerce.LocationId);

            if (Context.Entry(commerce).State != EntityState.Detached) { return; }

            Context.Entry(commerce).State = EntityState.Modified;
        }

        private void LocationUsedException(Guid commerceLocationId, Guid oldLocationId)
        {
            var locationAlreadyUsed = _context.Set<Node>()
                .Where(c => c.LocationId != oldLocationId)
                .Any(c => c.LocationId == commerceLocationId);

            if (locationAlreadyUsed)
                throw new DbUpdateException("The given location is being used by a Commerce");
        }
    }
}
