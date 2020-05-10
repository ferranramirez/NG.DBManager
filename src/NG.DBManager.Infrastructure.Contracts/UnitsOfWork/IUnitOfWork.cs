using NG.DBManager.Infrastructure.Contracts.Repositories;
using System;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITourRepository TourRepository { get; }

        int Commit();
    }
}
