using NG.DBManager.Infrastructure.Contracts.Repositories;

namespace NG.DBManager.Infrastructure.Contracts.UnitsOfWork
{
    public interface IAPIUnitOfWork : IFullUnitOfWork
    {
        ITourRepository Tour { get; }
        INodeRepository Node { get; }
        IUserRepository User { get; }
        ICouponRepository Coupon { get; }
    }
}
