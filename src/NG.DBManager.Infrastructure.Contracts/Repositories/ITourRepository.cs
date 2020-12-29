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
        Task<IEnumerable<TourWithDealType>> GetAllWithDealTypes(int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetAllWithDealTypesAndLocation(int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetFeatured(int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetLastOnesCreated(int numOfTours, int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetByTag(string filter, int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter, int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetByCommerceName(string filter, int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetByDealType(string filter, int? pageNumber = default, int? pageSize = default);
        Task<IEnumerable<TourWithDealType>> GetByEverything(string filter, int? pageNumber = default, int? pageSize = default);
    }
}
