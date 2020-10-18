using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class TourRepository : Repository<Tour>, ITourRepository
    {
        public TourRepository(DbContext context) : base(context) { }

        public override Tour Get(object id)
        {
            return DbSet
                .Where(t => t.Id == (Guid)id)
                .Include(t => t.TourTags)
                    .ThenInclude(tt => tt.Tag)
                .Include(t => t.Nodes)
                .SingleOrDefault();
        }

        public (Tour, IEnumerable<DealType>) GetWithDealTypes(Guid id)
        {
            var tour = Get(id);

            var dealType = tour.Nodes
                .Where(n => n.Deal?.DealType != null)
                .Select(n => n.Deal.DealType);

            return (tour, dealType);
        }

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetAllWithDealTypes()
        {
            var tours = await DbSet.ToListAsync();

            List<(Tour, IEnumerable<DealType>)> result = new List<(Tour, IEnumerable<DealType>)>();

            foreach (var tour in tours)
            {
                var dealTypes = tour.Nodes
                    .Where(n => n.Deal?.DealType != null)
                    .Distinct()
                    .Select(n => n.Deal.DealType);

                result.Add((tour, dealTypes));
            }

            return result;
        }

        public override void Add(Tour entity)
        {
            entity.Created = DateTime.UtcNow;
            DbSet.Add(entity);
            // Context.Entry(entity).Property("Created").CurrentValue = DateTime.UtcNow; // Add shadow property value
        }

        public async Task<IEnumerable<Tour>> GetFeatured()
        {
            return await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(t => t.IsFeatured)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetLastOnesCreated(int numOfTours)
        {
            return await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .OrderBy(t => t.Created)
                .Take(numOfTours)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByFullTag(string fullTag)
        {
            var LowCaseFilter = fullTag.ToLower();

            return await DbSet
                .Where(t => t.IsActive)
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
                .Where(t => t.IsActive)
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
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(tour => tour.Name.ToLower().Contains(LowCaseFilter)
                    || tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower().Contains(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByCommerceName(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            var commercesLocationIds = Context.Set<Commerce>()
                .Where(com => com.Name.ToLower().Contains(LowCaseFilter))
                .Select(c => c.LocationId);

            return await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(tour => tour.Nodes.Any(node =>
                    commercesLocationIds.Contains(node.LocationId)))
                .OrderBy(t => t.Name)
                .ToListAsync();
        }
    }
}
