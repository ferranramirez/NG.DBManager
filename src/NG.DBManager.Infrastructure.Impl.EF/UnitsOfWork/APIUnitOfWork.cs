using NG.Common.Services.AuthorizationProvider;
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
        private readonly IPasswordHasher _passwordHasher;

        public APIUnitOfWork(NgContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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

        public INodeRepository Node
        {
            get
            {
                if (_repositories[typeof(Node)] == null)
                {
                    _repositories[typeof(Node)] =
                        (INodeRepository)Activator.CreateInstance(typeof(NodeRepository), _context);
                }
                return (INodeRepository)_repositories[typeof(Node)];
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_repositories[typeof(User)] == null)
                {
                    _repositories[typeof(User)] =
                        (IUserRepository)Activator.CreateInstance(typeof(UserRepository), _context, _passwordHasher);
                }
                return (IUserRepository)_repositories[typeof(User)];
            }
        }
        public ICouponRepository Coupon
        {
            get
            {
                if (_repositories[typeof(Coupon)] == null)
                {
                    _repositories[typeof(Coupon)] =
                        (ICouponRepository)Activator.CreateInstance(typeof(CouponRepository), _context);
                }
                return (ICouponRepository)_repositories[typeof(Coupon)];
            }
        }

        public ICommerceRepository Commerce
        {
            get
            {
                if (_repositories[typeof(Commerce)] == null)
                {
                    _repositories[typeof(Commerce)] =
                        (ICommerceRepository)Activator.CreateInstance(typeof(CommerceRepository), _context);
                }
                return (ICommerceRepository)_repositories[typeof(Commerce)];
            }
        }

        public IDealRepository Deal
        {
            get
            {
                if (_repositories[typeof(Deal)] == null)
                {
                    _repositories[typeof(Deal)] =
                        (IDealRepository)Activator.CreateInstance(typeof(DealRepository), _context);
                }
                return (IDealRepository)_repositories[typeof(Deal)];
            }
        }

        public ITourTagRepository TourTag
        {
            get
            {
                if (_repositories[typeof(TourTag)] == null)
                {
                    _repositories[typeof(TourTag)] =
                        (ITourTagRepository)Activator.CreateInstance(typeof(TourTagRepository), _context);
                }
                return (ITourTagRepository)_repositories[typeof(TourTag)];
            }
        }
    }
}
