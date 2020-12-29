using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Linq;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class TourTagRepository : Repository<TourTag>, ITourTagRepository
    {
        public TourTagRepository(DbContext context) : base(context) { }

        public void Remove(Guid tourId, Guid tagId)
        {
            TourTag tourTagToDelete = DbSet
                .Where(tt => tt.TourId == tourId && tt.TagId == tagId)
                .SingleOrDefault();

            if (tourTagToDelete == null) { return; }

            DbSet.Remove(tourTagToDelete);
        }
    }
}
