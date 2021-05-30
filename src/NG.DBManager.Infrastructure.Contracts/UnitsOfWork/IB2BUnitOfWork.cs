using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Repositories;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IB2BUnitOfWork : IUnitOfWork
    {
        ICouponRepository Coupon { get; }
        IUserRepository User { get; }
        IVisitRepository Visit { get; }
        ICommerceRepository Commerce { get; }
        IRepository<Node> Node { get; }
    }
}
