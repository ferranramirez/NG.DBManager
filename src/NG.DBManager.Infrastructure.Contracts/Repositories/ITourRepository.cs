using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        TourWithDealType GetWithDealTypes(Guid id);
        Task<IEnumerable<TourWithDealType>> GetAllWithDealTypes();
        Task<IEnumerable<TourWithDealType>> GetAllWithDealTypesAndLocation();
        Task<IEnumerable<TourWithDealType>> GetFeatured();
        Task<IEnumerable<TourWithDealType>> GetLastOnesCreated(int numOfTours);
        Task<IEnumerable<TourWithDealType>> GetByTag(string filter);
        Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter);
        Task<IEnumerable<TourWithDealType>> GetByCommerceName(string filter);
        Task<IEnumerable<TourWithDealType>> GetByDealType(string filter);
        Task<IEnumerable<TourWithDealType>> GetByEverything(string filter);
    }
}
