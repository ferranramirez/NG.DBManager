using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        (Tour, IEnumerable<DealType>) GetWithDealTypes(Guid id);
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetAllWithDealTypes();
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetFeatured();
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetLastOnesCreated(int numOfTours);
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByTag(string filter);
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByFullTag(string fullTag);
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByTagOrName(string filter);
        Task<IEnumerable<(Tour, IEnumerable<DealType>)>> GetByCommerceName(string filter);
        Task<IEnumerable<TourWithDealType>> GetByDealType(string filter);
    }
}
