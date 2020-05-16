using System;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
