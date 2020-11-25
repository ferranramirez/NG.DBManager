﻿using NG.DBManager.Infrastructure.Contracts.Contexts;
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
                        (IUserRepository)Activator.CreateInstance(typeof(UserRepository), _context);
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
    }
}
