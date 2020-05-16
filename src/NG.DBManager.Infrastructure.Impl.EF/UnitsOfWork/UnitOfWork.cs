using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected bool _disposed;
        private readonly NgContext _context;

        protected UnitOfWork(NgContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
