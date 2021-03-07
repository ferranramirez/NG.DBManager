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
    public class UserRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IAPIUnitOfWork UnitOfWork;


        public UserRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            Context = databaseUtilities.GeneratePostgreSqlContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context, null);
        }

        [Fact]
        public async Task AddUser()
        {
            //ARRANGE
            User expected = new User
            {
                Id = new Guid(),
                Name = "Usersito",
                Email = "em@ail.com",
                Birthdate = DateTime.Now.AddYears(-18),
                Password = "asdadas",
                Role = DBManager.Infrastructure.Contracts.Models.Enums.Role.Commerce
            };

            //ACT
            UnitOfWork.User.Add(expected);
            await UnitOfWork.CommitAsync();

            var actual = UnitOfWork.User.Get(expected.Id);

            //ASSERT
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
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
