using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NG.DBManager.Test.UnitTest.SQLite
{
    public class UserRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IPasswordHasher passwordHasher;
        private readonly IAPIUnitOfWork UnitOfWork;
        private readonly IB2BUnitOfWork B2BUnitOfWork;


        public UserRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;


            passwordHasher = null;
            Context = databaseUtilities.GeneratePostgreSqlContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context, passwordHasher);
            B2BUnitOfWork = new B2BUnitOfWork(Context, passwordHasher);
        }

        [Fact]
        public void GetUserFromEmail()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var firstUser = _databaseUtilities.Users.FirstOrDefault();
            var expected = UnitOfWork.User.GetByEmail(firstUser.Email);

            //ACT
            var actual = UnitOfWork.User.Find(u => u.Email == firstUser.Email).Single();

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expected, expected);
        }

        [Fact]
        public async Task EditUserDetails()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var firstUser = _databaseUtilities.Users.FirstOrDefault();

            firstUser.Email = "updated@mail.com";
            firstUser.Name = "updated";
            firstUser.PhoneNumber = "+0000000000";
            firstUser.Password = "updated";

            //ACT
            var expected = UnitOfWork.User.Edit(firstUser);
            await UnitOfWork.CommitAsync();
            var actual = UnitOfWork.User.Get(firstUser.Id);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
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
