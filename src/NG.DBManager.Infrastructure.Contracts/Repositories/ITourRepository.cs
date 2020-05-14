using NG.DBManager.Infrastructure.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        Task<IEnumerable<Tour>> GetToursByTag(string filter);
        Task<IEnumerable<Tour>> GetToursByFullTag(string fullTag);
        Task<IEnumerable<Tour>> GetToursByTagOrName(string filter);
        Task<IEnumerable<Tour>> GetFeaturedTours();
        Task<IEnumerable<Tour>> GetLastTours(int numOfTours);
    }
}
