using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class NodeRepository : Repository<Node>, INodeRepository
    {
        private readonly DbContext _context;
        public NodeRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public override Node Get(object id)
        {
            return DbSet
                .Where(n => n.Id == (Guid)id)
                .Include(n => n.Deal)
                    .ThenInclude(n => n.DealType)
                .Include(n => n.Audios)
                .Include(n => n.Images)
                .Include(n => n.Location)
                .SingleOrDefault();
        }

        public override void Add(Node node)
        {
            if (node == null) { return; }

            var commerceSet = _context.Set<Commerce>();

            var commerceIds = commerceSet.Select(c => c.LocationId).ToList();

            if (node.Location.Nodes.Any(n => commerceIds.Contains(n.LocationId)))
                throw new DbUpdateException("The given location is being used by a Commerce");

            DbSet.Add(node);
        }
    }
}
