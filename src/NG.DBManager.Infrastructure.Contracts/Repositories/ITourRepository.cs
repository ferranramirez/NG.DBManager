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
        Task<IEnumerable<Tour>> GetFeatured();
        Task<IEnumerable<Tour>> GetLastOnesCreated(int numOfTours);
        Task<IEnumerable<Tour>> GetByTag(string filter);
        Task<IEnumerable<Tour>> GetByFullTag(string fullTag);
        Task<IEnumerable<Tour>> GetByTagOrName(string filter);
        Task<IEnumerable<Tour>> GetByCommerceName(string filter);
    }
}
