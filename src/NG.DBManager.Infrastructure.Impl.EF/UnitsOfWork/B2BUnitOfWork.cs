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

        private IRepository<Coupon> _couponRepository;

        public B2BUnitOfWork(NgContext context) : base(context)
        {
            _context = context;
        }

        public IRepository<Coupon> Coupon
        {
            get
            {
                if (_couponRepository == null)
                {
                    return (_couponRepository =
                        (IRepository<Coupon>)Activator.CreateInstance(typeof(Repository<Coupon>), _context));
                }
                return _couponRepository;
            }
        }
    }
}
