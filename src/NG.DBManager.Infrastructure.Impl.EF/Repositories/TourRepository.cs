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

        public async Task<IEnumerable<Tour>> GetFeatured()
        {
            return await DbSet
                .AsNoTracking()
                .Where(t => t.IsFeatured)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetLastOnesCreated(int numOfTours)
        {
            return await DbSet
                .AsNoTracking()
                .OrderBy(t => Property<DateTime>(t, "Created"))
                .Take(numOfTours)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByFullTag(string fullTag)
        {
            var LowCaseFilter = fullTag.ToLower();

            return await DbSet
                .AsNoTracking()
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower()
                        .Equals(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByTag(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            return await DbSet
                .AsNoTracking()
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower()
                        .Contains(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByTagOrName(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            return await DbSet
                .AsNoTracking()
                .Where(tour => tour.Name.ToLower().Contains(LowCaseFilter)
                    || tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower().Contains(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .ToListAsync();
        }
    }
}
