using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface INodeRepository : IRepository<Node>
    {
        IEnumerable<Node> GetNodesFromTour(Guid tourId);
    }
}
