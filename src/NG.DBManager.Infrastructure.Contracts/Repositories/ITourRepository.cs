using NG.DBManager.Infrastructure.Contracts.Models;
using System.Collections.Generic;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        IEnumerable<Tour> GetToursByTag(string filter);
        IEnumerable<Tour> GetToursByTagOrName(string filter);
        IEnumerable<Tour> GetFeaturedTours();
        IEnumerable<Tour> GetLastTours(int numOfTours);
    }
}
