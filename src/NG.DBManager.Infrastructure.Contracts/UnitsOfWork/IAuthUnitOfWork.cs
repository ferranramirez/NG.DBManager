using NG.DBManager.Infrastructure.Contracts.Repositories;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IAuthUnitOfWork : IUnitOfWork
    {
        IUserRepository User { get; }
    }
}
