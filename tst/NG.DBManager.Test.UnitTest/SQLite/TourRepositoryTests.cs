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
    public class TourRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IAPIUnitOfWork UnitOfWork;


        public TourRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            Context = databaseUtilities.GenerateSQLiteContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context);
        }


        [Fact]
        public async Task AddTour()
        {
            //ARRANGE
            Guid newTourId = Guid.NewGuid();
            Tour newTour = new Tour
            {
                Id = newTourId,
                Name = "My add test Tour",
                Description = "A nice description for such an interesting Tour",
            };

            //ACT
            UnitOfWork.Tour.Add(newTour);
            await UnitOfWork.CommitAsync();

            //ASSERT
            var tourFromDb = UnitOfWork.Tour.Get(newTourId);

            Assert.NotNull(tourFromDb);
            Assert.Equal(tourFromDb, newTour);

            var createdProperty = tourFromDb.Created; //Context.Entry(tourFromDb).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);
        }

        [Fact]
        public async Task GetAllFeaturedTours()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);
            var expected = _databaseUtilities.Tours
                            .Where(t => t.IsFeatured)
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetFeatured();

            //ASSERT
            Assert.Equal(expected, actual.ToList());
        }

        [Fact]
        public async Task GetLastOnesCreated()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            //ACT
            var actual = await UnitOfWork.Tour.GetLastOnesCreated(1);


            //ASSERT
            var lastTourCreated = actual.ToList().Single();
            Assert.NotNull(lastTourCreated);

            var createdProperty = Context.Entry(lastTourCreated).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);
        }

        [Fact]
        public async Task GetToursByTagAsync()
        {
            //ARRANGE
            // _database.FullTagName = "Supercalifragilisticexpialidocious"

            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(t => t.TourTags
                                .Any(tt => tt.Tag.Name
                                    .Equals("Supercalifragilisticexpialidocious",
                                        StringComparison.CurrentCultureIgnoreCase)))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetByTag("Supercalifragilisticexpialidocious");

            //ASSERT
            Assert.Equal(expected, actual.ToList());
        }

        [Fact]
        public async Task GetToursByTag()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(t => t.TourTags
                                .Any(tt => tt.Tag.Name
                                    .Contains("CaliFRAGIListIcexpIaLidoc",
                                        StringComparison.CurrentCultureIgnoreCase)))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetByTag("CaliFRAGIListIcexpIaLidoc");

            //ASSERT
            Assert.Equal(expected, actual.ToList());
        }


        [Fact]
        public async Task GetByTagOrName()
        {
            //ARRANGE
            // _database.TourExistingName = "Custom Tour, Random But Unique Name"
            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(tour => tour.Name
                                .Contains("Tour, Random But Unique",
                                    StringComparison.CurrentCultureIgnoreCase))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetByTagOrName("Tour, Random But Unique");

            //ASSERT
            Assert.Equal(expected, actual.ToList());
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
