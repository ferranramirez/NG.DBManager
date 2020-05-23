using NG.DBManager.Infrastructure.Contracts.Models;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string emailAddress);
    }
}
