using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class NodeRepository : Repository<Node>, INodeRepository
    {
        public NodeRepository(DbContext context) : base(context) { }

        public override Node Get(object id)
        {
            return DbSet
                .Where(n => n.Id == (Guid)id)
                .Include(n => n.Location)
                .SingleOrDefault();
        }
    }
}
