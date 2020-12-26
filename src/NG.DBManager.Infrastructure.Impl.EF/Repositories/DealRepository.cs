using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class DealRepository : Repository<Deal>, IDealRepository
    {
        public DealRepository(DbContext context) : base(context) { }

        public override Deal Get(object id)
        {
            return DbSet
                .Where(n => n.Id == (Guid)id)
                .Include(n => n.DealType)
                .SingleOrDefault();
        }
    }
}
