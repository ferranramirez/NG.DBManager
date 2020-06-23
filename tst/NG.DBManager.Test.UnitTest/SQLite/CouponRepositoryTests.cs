using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NG.DBManager.Test.UnitTest.SQLite
{
    public class CouponRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IAPIUnitOfWork UnitOfWork;


        public CouponRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            Context = databaseUtilities.GenerateSQLiteContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context);
        }

        [Fact]
        public void AddCoupon()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            Guid newCouponId = Guid.NewGuid();
            Guid userId = _databaseUtilities.Users.First().Id;
            Guid nodeId = _databaseUtilities.Nodes.First().Id;

            Coupon newCoupon = new Coupon
            {
                Id = newCouponId,
                UserId = userId,
                Content = "{Coupon content test}",
                NodeId = nodeId,
            };

            //ACT
            UnitOfWork.Repository<Coupon>().Add(newCoupon);
            UnitOfWork.CommitAsync();

            //ASSERT
            var couponFromDb = UnitOfWork.Repository<Coupon>().Get(newCouponId);
            Assert.NotNull(couponFromDb);
            Assert.Equal(couponFromDb, newCoupon);
        }

        [Fact]
        public async Task GetAllCoupons()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);
            Guid couponId = _databaseUtilities.Coupons.First().Id;

            //ACT
            var coupons = await UnitOfWork.Repository<Coupon>().GetAll();
            var fisrtCoupon = UnitOfWork.Repository<Coupon>().Get(couponId);

            //ASSERT
            Assert.NotNull(coupons);
        }

        // Dispose pattern 
        private bool _disposed;
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                Context.Database.EnsureDeleted();
                Context.Dispose();
                UnitOfWork.Dispose();
            }
            _disposed = true;
        }
    }
}
