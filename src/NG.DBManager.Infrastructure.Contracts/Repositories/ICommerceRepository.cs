using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Repositories
{
    public interface ICommerceRepository : IRepository<Commerce>
    {
        public Commerce GetByCoupon(Guid couponId);
    }
}
