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

            if (commerce.Location.Nodes.Any())
                throw new DbUpdateException("The selected location is being used by another node");

            DbSet.Add(commerce);
        }
    }
}
