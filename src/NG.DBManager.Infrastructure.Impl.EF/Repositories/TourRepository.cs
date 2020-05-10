using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.EF;

namespace NG.DBManager.Infrastructure.Impl.EF.Repositories
{
    public class TourRepository : Repository<NgContext, Tour>, ITourRepository
    {
        public TourRepository(NgContext context) : base(context) { }

        public override void Add(Tour entity)
        {
            DbSet.Add(entity);
            Context.Entry(entity).Property("Created").CurrentValue = DateTime.UtcNow;
        }

        public IEnumerable<Tour> GetFeaturedTours()
        {
            //return DbSet
            //    .Where(t => t.Featured != null)
            //    .ToList();

            return Context.Featured
                .SelectMany(ft => ft.Tours)
                .ToList();
        }

        public IEnumerable<Tour> GetLastTours(int numOfTours)
        {
            return DbSet
                .OrderBy(t => Property<DateTime>(t, "LastUpdated"))
                .Take(numOfTours)
                .ToList();
        }

        public IEnumerable<Tour> GetToursByTag(string filter)
        {
            return DbSet
                .Where(tour => tour.TourTags
                    .Any(tourTag => tourTag.Tag.Name.Contains(filter)))
                .ToList();
        }

        public IEnumerable<Tour> GetToursByTagOrName(string filter)
        {
            return DbSet
                .Where(tour => tour.Name.Contains(filter)
                    || tour.TourTags.Any(tourTag => tourTag.Tag.Name.Contains(filter)))
                .ToList();
        }
    }
}
