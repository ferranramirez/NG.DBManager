using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Repositories;
using System;
using System.Collections;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class APIUnitOfWork : FullUnitOfWork, IAPIUnitOfWork
    {
        private readonly NgContext _context;
        private Hashtable _repositories;

        public APIUnitOfWork(NgContext context) : base(context)
        {
            _context = context;
            _repositories = new Hashtable();
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

        public IUserRepository User
        {
            get
            {
                if (_repositories[typeof(User)] == null)
                {
                    _repositories[typeof(User)] =
                        (IUserRepository)Activator.CreateInstance(typeof(UserRepository), _context);
                }
                return (IUserRepository)_repositories[typeof(User)];
            }
        }
    }
}
