using NG.DBManager.Infrastructure.Contracts.Contexts;
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

        public B2BUnitOfWork(NgContext context) : base(context)
        {
            _context = context;
        }

        public ICouponRepository Coupon
        {
            get
            {
                return _couponRepository ?? (_couponRepository = (ICouponRepository)Activator.CreateInstance(typeof(CouponRepository), _context));
            }
        }
    }
}
