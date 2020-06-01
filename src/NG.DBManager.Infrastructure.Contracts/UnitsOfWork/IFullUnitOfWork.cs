using NG.DBManager.Infrastructure.Contracts.Repositories;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IFullUnitOfWork : IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
    }
}
