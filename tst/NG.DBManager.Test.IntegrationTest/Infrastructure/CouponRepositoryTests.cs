using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NG.DBManager.Test.IntegrationTest.Infrastructure
{
    public class CouponRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IPasswordHasher passwordHasher;
        private readonly IAPIUnitOfWork UnitOfWork;
        private readonly IB2BUnitOfWork B2BUnitOfWork;


        public CouponRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            passwordHasher = null;
            Context = databaseUtilities.GeneratePostgreSqlContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context, passwordHasher);
            B2BUnitOfWork = new B2BUnitOfWork(Context, passwordHasher);
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
            Context.Database.EnsureCreated();
            UnitOfWork.CommitAsync();

            //ASSERT
            using (var assertContext = _databaseUtilities.GeneratePostgreSqlContext())
            {
                var assertUOW = new APIUnitOfWork(assertContext, passwordHasher);
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
            using (var assertContext = _databaseUtilities.GeneratePostgreSqlContext())
            {
                var assertUOW = new APIUnitOfWork(assertContext, passwordHasher);
                var couponFromDb = assertUOW.Repository<Coupon>().Get(firstCoupon.Id);
                Assert.NotNull(couponFromDb);
                Assert.Equal(couponFromDb, firstCoupon);
            }
        }

        [Fact]
        public async Task ValidateNodeDeal()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var firstCommerceNode = _databaseUtilities.Nodes.First(n => n.Deal != null);
            var nodeCoupon = _databaseUtilities.Coupons.First(c => c.NodeId == firstCommerceNode.Id);

            var expected = firstCommerceNode.Deal;

            //ACT
            var nodeDb = B2BUnitOfWork.Node.Find(c => c.Id == firstCommerceNode.Id).Single();
            var couponDb = B2BUnitOfWork.Coupon.Find(c => c.Id == nodeCoupon.Id).Single();
            var actual = nodeDb.Deal;

            //ASSERT
            Assert.Equal(nodeDb.Deal, couponDb.Node.Deal);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetCommerceUserFromCoupon()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var validatedCoupons = _databaseUtilities.Coupons.Where(c => !c.IsValidated);
            var firstCoupon = validatedCoupons.FirstOrDefault();
            var couponLocationId = validatedCoupons.Select(c => c.Node.LocationId).FirstOrDefault();
            var expected = _databaseUtilities.Commerces.FirstOrDefault(com => com.LocationId == couponLocationId).User;

            var couponDb = UnitOfWork.Repository<Coupon>().Get(firstCoupon.Id);

            //ACT
            var actual = B2BUnitOfWork.Commerce.Find(c => c.LocationId == couponLocationId).Select(com => com.User).Single();

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetLastCouponFromUserAndNode()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var refCoupon = _databaseUtilities.Coupons.FirstOrDefault();
            var expected = _databaseUtilities.Coupons
                .Where(c => c.UserId == refCoupon.UserId && c.NodeId == refCoupon.NodeId)
                .OrderBy(c => c.GenerationDate)
                .FirstOrDefault();

            //ACT
            var actual = await B2BUnitOfWork.Coupon.GetLastByNode(refCoupon.UserId, refCoupon.NodeId);

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetByCommerce()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var refCoupon = _databaseUtilities.Coupons.FirstOrDefault();
            //var expected = _databaseUtilities.Coupons
            //    .Where(c => c.UserId == refCoupon.UserId && c.NodeId == refCoupon.NodeId)
            //    .OrderBy(c => c.GenerationDate)
            //    .FirstOrDefault();

            var commerce = _databaseUtilities.Commerces.FirstOrDefault();

            //ACT
            var actual = await UnitOfWork.Coupon.GetByCommerce(commerce.Id);

            //ASSERT
            Assert.True(false);
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
