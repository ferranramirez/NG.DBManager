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

            LocationUsedException(node, default);

            DbSet.Add(node);
        }

        public override void Update(Node node)
        {
            if (node == null) { return; }

            var oldNode = DbSet.Find(node);
            LocationUsedException(node, oldNode.LocationId);

            if (Context.Entry(node).State != EntityState.Detached) { return; }

            Context.Entry(node).State = EntityState.Modified;
        }

        private void LocationUsedException(Node node, Guid oldNodeLocationId)
        {
            var commerceSet = _context.Set<Commerce>();
            var commerceIds = commerceSet
                .Where(c => c.LocationId != oldNodeLocationId)
                .Select(c => c.LocationId)
                .ToList();

            var location = _context.Set<Location>().Find(node.LocationId);

            if (commerceIds.Contains(node.LocationId))
                throw new DbUpdateException("The given location is being used by a Commerce");
        }
    }
}
