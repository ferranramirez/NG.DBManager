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
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private readonly DbContext _context;

        public VisitRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VisitInfo>> GetByCommerce(Guid CommerceId)
        {
            var commerce = _context.Set<Commerce>().SingleOrDefault(com => com.Id == CommerceId);

            if (commerce == null) return null;

            var VisitInfo = await DbSet
                .Where(vh => vh.CommerceId == commerce.Id)
                .Select(vh =>
                new VisitInfo
                {
                    TourInfo = new TourInfo { Id = vh.TourId, Name = vh.Tour.Name },
                    Deal = GetTourDeal(vh.Tour, vh.Commerce),
                    UserInfo = new UserInfo { Name = vh.User.Name, Email = vh.User.Email },
                    RegistryDate = vh.RegistryDate,
                })
                .ToListAsync();

            return VisitInfo;
        }

        private static Deal GetTourDeal(Tour tour, Commerce commerce)
        {
            return tour.Nodes
                .Where(n => n.LocationId == commerce.LocationId && n.Deal?.DealType != null)
                .Select(n => n.Deal)
                .SingleOrDefault();
        }
    }
}
