﻿using Microsoft.EntityFrameworkCore;
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

        public TourWithDealType GetWithDealTypes(Guid id)
        {
            var tour = Get(id);

            return GetSingleTourWithDealTypes(tour);
        }

        public async Task<IEnumerable<TourWithDealType>> GetAllWithDealTypes()
        {
            var tours = await DbSet
                .Include(t => t.Nodes)
                    .ThenInclude(n => n.Deal)
                        .ThenInclude(d => d.DealType)
                .ToListAsync();

            return GetToursWithDealTypes(tours);
        }

        public override void Add(Tour entity)
        {
            entity.Created = DateTime.UtcNow;
            DbSet.Add(entity);
            // Context.Entry(entity).Property("Created").CurrentValue = DateTime.UtcNow; // Add shadow property value
        }

        public async Task<IEnumerable<TourWithDealType>> GetFeatured()
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

        public async Task<IEnumerable<TourWithDealType>> GetLastOnesCreated(int numOfTours)
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

            return GetToursWithDealTypes(tours);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByFullTag(string fullTag)
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

        public async Task<IEnumerable<TourWithDealType>> GetByTag(string filter)
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

        public async Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter)
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

        public async Task<IEnumerable<TourWithDealType>> GetByCommerceName(string filter)
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

            return GetToursWithDealTypes(tours);
        }

        private static List<TourWithDealType> GetToursWithDealTypes(List<Tour> tours)
        {
            List<TourWithDealType> result = new List<TourWithDealType>();

            foreach (var tour in tours)
            {
                TourWithDealType tourWithDealType = GetSingleTourWithDealTypes(tour);

                result.Add(tourWithDealType);
            }

            return result;
        }

        private static TourWithDealType GetSingleTourWithDealTypes(Tour tour)
        {
            return new TourWithDealType(tour)
            {
                DealTypes = tour.Nodes
                                    .Where(n => n.Deal?.DealType != null)
                                    .Distinct()
                                    .Select(n => n.Deal?.DealType)
            };
        }
    }
}
