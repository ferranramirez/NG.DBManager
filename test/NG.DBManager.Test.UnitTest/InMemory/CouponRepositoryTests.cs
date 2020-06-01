﻿using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using Xunit;

namespace NG.DBManager.Test.UnitTest.InMemory
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

            Context = databaseUtilities.GenerateInMemoryContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context);
        }

        [Fact]
        public void AddCoupon()
        {
            //ARRANGE
            _databaseUtilities.Seed(Context);

            Guid newCouponId = Guid.NewGuid();
            Guid userId = _databaseUtilities.Users.First().Id;
            Guid commerceId = _databaseUtilities.Commerces.First().Id;

            Coupon newCoupon = new Coupon
            {
                Id = newCouponId,
                UserId = userId,
                Content = "{Coupon content test}",
                CommerceId = commerceId,
            };

            //ACT
            UnitOfWork.Repository<Coupon>().Add(newCoupon);
            UnitOfWork.CommitAsync();

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
        public void ValidateCoupon()
        {
            //ARRANGE
            _databaseUtilities.Seed(Context);

            var firstCoupon = _databaseUtilities.Coupons.First(c => !c.IsValidated);

            //var couponDb = UnitOfWork.Repository<Coupon>().Get(firstCoupon.Id);
            firstCoupon.ValidationDate = DateTime.Now;

            //ACT
            UnitOfWork.Repository<Coupon>().Update(firstCoupon);
            UnitOfWork.CommitAsync();

            //ASSERT
            using (var assertContext = _databaseUtilities.GenerateInMemoryContext())
            {
                var assertUOW = new APIUnitOfWork(assertContext);
                var couponFromDb = assertUOW.Repository<Coupon>().Get(firstCoupon.Id);
                Assert.NotNull(couponFromDb);
                Assert.Equal(couponFromDb, firstCoupon);
            }
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
