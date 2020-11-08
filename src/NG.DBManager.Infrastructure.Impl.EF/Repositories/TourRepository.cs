using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Entities;
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
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .SingleOrDefault();
        }

        public (Tour, IEnumerable<DealType>) GetWithDealTypes(Guid id)
        {
            var tour = Get(id);

            var dealType = tour
                .Nodes
                .Where(n => n.Deal?.DealType != null)

                .Select(n => n.Deal.DealType);

            return (tour, dealType);
        }

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetAllWithDealTypes()
        {
            var tours = await DbSet
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

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

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetFeatured()
        {
            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(t => t.IsFeatured)
                .OrderBy(t => t.Name)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

            return GetToursWithDealTypes(tours);
        }

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetLastOnesCreated(int numOfTours)
        {
            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .OrderBy(t => t.Created)
                .Take(numOfTours)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

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

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByFullTag(string fullTag)
        {
            var LowCaseFilter = fullTag.ToLower();

            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower()
                        .Equals(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

            return GetToursWithDealTypes(tours);
        }

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByTag(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower()
                        .Contains(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

            return GetToursWithDealTypes(tours);
        }

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByTagOrName(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(tour => tour.Name.ToLower().Contains(LowCaseFilter)
                    || tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.ToLower().Contains(LowCaseFilter)))
                .OrderBy(t => t.Name)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

            return GetToursWithDealTypes(tours);
        }

        public async Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByCommerceName(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            var commercesLocationIds = Context.Set<Commerce>()
                .Where(com => com.Name.ToLower().Contains(LowCaseFilter))
                .Select(c => c.LocationId);

            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .Where(tour => tour.Nodes.Any(node =>
                    commercesLocationIds.Contains(node.LocationId)))
                .OrderBy(t => t.Name)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

            return GetToursWithDealTypes(tours);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByDealType(string filter)
        {
            var LowCaseFilter = filter.ToLower();

            var dealTypeIds = Context.Set<DealType>()
                .Where(dt => dt.Name.ToLower().Contains(LowCaseFilter))
                .Select(dt => dt.Id)
                .ToList();

            var tours = await DbSet
                .Where(t => t.IsActive)
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .Where(tour => tour.Nodes.Any(node => node.Deal != null &&
                    dealTypeIds.Contains(node.Deal.DealTypeId != null ? (Guid)node.Deal.DealTypeId : Guid.Empty)))
                .ToListAsync();

            return GetToursWithDealTypesWithEntity(tours);
        }

        private static List<(Tour, IEnumerable<DealType>)> GetToursWithDealTypes(List<Tour> tours)
        {
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
        private static List<TourWithDealType> GetToursWithDealTypesWithEntity(List<Tour> tours)
        {
            List<TourWithDealType> result = new List<TourWithDealType>();

            foreach (var tour in tours)
            {
                TourWithDealType tourWithDealType = new TourWithDealType(tour);

                tourWithDealType.DealTypes = tour.Nodes
                    .Where(n => n.Deal?.DealType != null)
                    .Distinct()
                    .Select(n => n.Deal?.DealType);

                result.Add(tourWithDealType);
            }

            return result;
        }
    }
}
