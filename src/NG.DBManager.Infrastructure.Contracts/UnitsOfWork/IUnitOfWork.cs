using System;
using System.Threading.Tasks;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
