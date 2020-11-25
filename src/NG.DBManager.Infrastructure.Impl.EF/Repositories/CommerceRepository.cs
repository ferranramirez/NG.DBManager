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
        public CommerceRepository(DbContext context) : base(context) { }

        public override async Task<IEnumerable<Commerce>> GetAll(Expression<Func<Commerce, object>>[] includes)
        {
            return await DbSet
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
}
