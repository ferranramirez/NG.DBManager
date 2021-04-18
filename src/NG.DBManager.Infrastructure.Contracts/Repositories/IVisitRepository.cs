using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<VisitInfo>> GetByCommerce(Guid CommerceId);
    }
}
