using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Repositories;
using System;
using System.Collections;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NgContext _context;

        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(NgContext context)
        {
            _context = context;

            _repositories = new Hashtable();
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public ITourRepository Tour
        {
            get
            {
                if (_repositories[typeof(Tour)] == null)
                {
                    _repositories[typeof(Tour)] =
                        (ITourRepository)Activator.CreateInstance(typeof(TourRepository), _context);
                }
                return (ITourRepository)_repositories[typeof(Tour)];
            }
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return (IRepository<T>)GetRepository<T>(typeof(Repository<T>));
        }

        protected object GetRepository<T>(Type repositoryType) where T : class
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                object repositoryInstance = GetNewInstance<T>(repositoryType);

                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type];
        }

        private object GetNewInstance<T>(Type repositoryType) where T : class
        {
            if (repositoryType.IsGenericTypeDefinition)
            {
                return Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            }
            else
            {
                return Activator.CreateInstance(repositoryType, _context);
            }
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
