using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.EF;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(DbContext context) : base(context) { }

        public override void Add(Tour entity)
        {
            DbSet.Add(entity);
            Context.Entry(entity).Property("Created").CurrentValue = DateTime.UtcNow;
        }

        public async Task<IEnumerable<Tour>> GetFeaturedTours()
        {
            return await DbSet
                .AsNoTracking()
                .Where(t => t.Featured != null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetLastTours(int numOfTours)
        {
            return await DbSet
                .AsNoTracking()
                .OrderBy(t => Property<DateTime>(t, "LastUpdated"))
                .Take(numOfTours)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetToursByTag(string filter)
        {
            return await DbSet
                .AsNoTracking()
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name
                        .Contains(filter, StringComparison.InvariantCultureIgnoreCase)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetToursByFullTag(string fullTag)
        {
            return await DbSet
                .AsNoTracking()
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name
                        .Equals(fullTag, StringComparison.InvariantCultureIgnoreCase)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetToursByTagOrName(string filter)
        {
            return await DbSet
                .AsNoTracking()
                .Where(tour => tour.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)
                    || tour.TourTags
                        .Any(tourTag => tourTag.Tag.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)))
                .ToListAsync();
        }
    }
}
