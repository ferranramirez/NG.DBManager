using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NG.DBManager.Test.UnitTest.InMemory
{
    public class CouponRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IAPIUnitOfWork UnitOfWork;
        private readonly IB2BUnitOfWork B2BUnitOfWork;


        public CouponRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            Context = databaseUtilities.GenerateInMemoryContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context);
            B2BUnitOfWork = new B2BUnitOfWork(Context);
        }

        [Fact]
        public async Task AddCoupon()
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
            await UnitOfWork.CommitAsync();

            //ASSERT
            using (var assertContext = _databaseUtilities.GenerateInMemoryContext())
            {
                var assertUOW = new APIUnitOfWork(assertContext);
                var couponFromDb = assertUOW.Repository<Coupon>().Get(newCouponId);
                Assert.NotNull(couponFromDb);
                Assert.Equal(couponFromDb, newCoupon);
            }
        }

        [Fact]
        public async Task ValidateCoupon()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var firstCoupon = _databaseUtilities.Coupons.First(c => !c.IsValidated);

            //var couponDb = UnitOfWork.Repository<Coupon>().Get(firstCoupon.Id);
            firstCoupon.ValidationDate = DateTime.Now;

            //ACT
            UnitOfWork.Repository<Coupon>().Update(firstCoupon);
            await UnitOfWork.CommitAsync();

            //ASSERT
            using (var assertContext = _databaseUtilities.GenerateInMemoryContext())
            {
                var assertUOW = new APIUnitOfWork(assertContext);
                var couponFromDb = assertUOW.Repository<Coupon>().Get(firstCoupon.Id);
                Assert.NotNull(couponFromDb);
                Assert.Equal(couponFromDb, firstCoupon);
            }
        }

        [Fact]
        public async Task GetCommerceUserFromCoupon()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var firstCoupon = _databaseUtilities.Coupons.First(c => !c.IsValidated);
            var firstCouponCommerceUser = firstCoupon.Node.Location.Commerce.User;

            //var couponDb = UnitOfWork.Repository<Coupon>().Get(firstCoupon.Id);
            firstCoupon.ValidationDate = DateTime.Now;

            //ACT
            var commerceUser = B2BUnitOfWork.Coupon.GetCommerceUser(firstCoupon.NodeId);

            //ASSERT
            Assert.NotNull(commerceUser);
            Assert.Equal(firstCouponCommerceUser, commerceUser);
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
