using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface ICMSUnitOfWork : IDisposable
    {
        int Commit();
        IRepository<T> GetRepository<T>() where T : class;
    }
}
