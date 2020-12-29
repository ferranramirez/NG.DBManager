using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ITourTagRepository : IRepository<TourTag>
    {
        void Remove(Guid tourId, Guid tagId);
    }
}
