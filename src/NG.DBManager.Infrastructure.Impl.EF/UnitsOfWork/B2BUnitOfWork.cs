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

        private ICouponRepository _couponRepository;
        private IRepository<Commerce> _commerceRepository;
        private IRepository<Node> _nodeRepository;

        public B2BUnitOfWork(NgContext context) : base(context)
        {
            _context = context;
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
