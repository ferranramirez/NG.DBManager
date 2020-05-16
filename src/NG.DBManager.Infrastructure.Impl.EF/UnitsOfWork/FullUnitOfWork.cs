using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Repositories;
using System;
using System.Collections;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class FullUnitOfWork : UnitOfWork, IFullUnitOfWork
    {
        private readonly NgContext _context;
        private Hashtable _repositories;

        public FullUnitOfWork(NgContext context) : base(context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return (IRepository<T>)GetRepository<T>(typeof(Repository<T>));
        }

        protected object GetRepository<T>(Type repositoryType) where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

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
    }
}
