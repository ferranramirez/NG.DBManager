using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.UnitTest.Infrastructure.Fixture;
using System;
using System.Linq;
using Xunit;

namespace NG.DBManager.Test.UnitTest.Infrastructure
{
    public class CouponRepositoryTests : IClassFixture<FullDbSetup>
    {
        private readonly FullDbSetup _dbSetup;

        private readonly DBManager.Infrastructure.Contracts.Contexts.NgContext Context;
        private readonly IUnitOfWork unitOfWork;


        public CouponRepositoryTests(FullDbSetup dbSetup)
        {
            _dbSetup = dbSetup;

            Context = _dbSetup.GenerateInMemoryContext();
            unitOfWork = new UnitOfWork(Context);
        }


        [Fact]
        public void AddCoupon()
        {
            //ARRANGE
            Guid newCouponId = Guid.NewGuid();
            var firstUserId = _dbSetup.Users.First().Id;
            var firstCommerceId = _dbSetup.Restaurants.First().CommerceId;

            var newCoupon = new Coupon()
            {
                Id = newCouponId,
                UserId = firstUserId,
                CommerceId = firstCommerceId,
                Content = "{Test Discount}",
                Redemption = default,
            };

            var couponRepo = unitOfWork.Repository<Coupon>();

            //ACT
            var couponDb = unitOfWork.Repository<Coupon>().Get(newCouponId);

            if (couponDb == null)
            {
                couponRepo.Add(newCoupon);
                unitOfWork.Commit();
            }

            //ASSERT
            var couponFromDb = unitOfWork.Repository<Coupon>().Get(newCouponId);
            Assert.NotNull(couponFromDb);
            Assert.Equal(couponFromDb, newCoupon);


            var options = new DbContextOptionsBuilder<NgContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using var context = new NgContext(options);
            var createdProperty = context.Entry(couponFromDb).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);

        }
    }
}
