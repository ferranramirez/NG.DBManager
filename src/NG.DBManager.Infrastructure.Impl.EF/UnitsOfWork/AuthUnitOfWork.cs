using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class AuthUnitOfWork : UnitOfWork, IAuthUnitOfWork
    {
        private readonly NgContext _context;

        private IRepository<User> _couponRepository;

        public AuthUnitOfWork(NgContext context) : base(context)
        {
            _context = context;
        }

        public IRepository<User> User
        {
            get
            {
                if (_couponRepository == null)
                {
                    return (_couponRepository =
                        (IRepository<User>)Activator.CreateInstance(typeof(IRepository<User>), _context));
                }
                return _couponRepository;
            }
        }
    }
}
