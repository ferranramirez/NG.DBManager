using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Repositories;
using System;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class B2BUnitOfWork : UnitOfWork, IB2BUnitOfWork
    {
        private readonly NgContext _context;
        private readonly IPasswordHasher _passwordHasher;

        private ICouponRepository _couponRepository;
        private IUserRepository _userRepository;
        private IVisitRepository _visitRepository;
        private IRepository<Commerce> _commerceRepository;
        private IRepository<Node> _nodeRepository;

        public B2BUnitOfWork(NgContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public ICouponRepository Coupon
        {
            get
            {
                if (_couponRepository == null)
                {
                    return (_couponRepository =
                        (ICouponRepository)Activator.CreateInstance(typeof(CouponRepository), _context));
                }
                return _couponRepository;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                {
                    return (_userRepository =
                        (IUserRepository)Activator.CreateInstance(typeof(UserRepository), _context, _passwordHasher));
                }
                return _userRepository;
            }
        }
        public IVisitRepository Visit
        {
            get
            {
                if (_visitRepository == null)
                {
                    return (_visitRepository =
                        (IVisitRepository)Activator.CreateInstance(typeof(VisitRepository), _context));
                }
                return _visitRepository;
            }
        }

        public IRepository<Commerce> Commerce
        {
            get
            {
                if (_commerceRepository == null)
                {
                    return (_commerceRepository =
                        (IRepository<Commerce>)Activator.CreateInstance(typeof(Repository<Commerce>), _context));
                }
                return _commerceRepository;
            }
        }

        public IRepository<Node> Node
        {
            get
            {
                if (_nodeRepository == null)
                {
                    return (_nodeRepository =
                        (IRepository<Node>)Activator.CreateInstance(typeof(Repository<Node>), _context));
                }
                return _nodeRepository;
            }
        }
    }
}
