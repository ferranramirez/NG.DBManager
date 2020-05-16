using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IAuthUnitOfWork : IUnitOfWork
    {
        IRepository<User> User { get; }
    }
}
