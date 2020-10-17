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
    public class TourRepositoryTests : IDisposable,
        IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IAPIUnitOfWork UnitOfWork;

        public TourRepositoryTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            Context = databaseUtilities.GeneratePostgreSqlContext();
            Context.Database.EnsureCreated();
            UnitOfWork = new APIUnitOfWork(Context);
        }

        [Fact]
        public async Task AddTour()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            Guid newTourId = Guid.NewGuid();
            Tour newTour = new Tour
            {
                Id = newTourId,
                Name = "My add test Tour",
                Description = "A nice description for such an interesting Tour"
            };

            //ACT
            UnitOfWork.Tour.Add(newTour);
            await UnitOfWork.CommitAsync();

            //ASSERT
            using (var assertContext = _databaseUtilities.GeneratePostgreSqlContext())
            {
                var assertUOW = new APIUnitOfWork(assertContext);
                var tourFromDb = assertUOW.Tour.Get(newTourId);

                Assert.NotNull(tourFromDb);
                Assert.Equal(tourFromDb, newTour);

                var createdProperty = tourFromDb.Created; //Context.Entry(tourFromDb).Property("Created").CurrentValue;
                Assert.NotNull(createdProperty);
            }
        }

        [Fact]
        public void GetAllFeaturedTours()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);
            var expected = _databaseUtilities.Tours
                            .Where(t => t.IsFeatured)
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = UnitOfWork.Tour.GetFeatured().Result;

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetLastOnesCreated()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            //ACT
            var actual = UnitOfWork.Tour.GetLastOnesCreated(1).Result;


            //ASSERT
            var lastTourCreated = actual.Single();
            Assert.NotNull(lastTourCreated);

            var createdProperty = Context.Entry(lastTourCreated).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);
        }

        [Fact]
        public void GetToursByFullTag()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(t => t.TourTags
                                .Any(tt => tt.Tag.Name
                                    .Equals("Supercalifragilisticexpialidocious",
                                        StringComparison.CurrentCultureIgnoreCase)))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = UnitOfWork.Tour.GetByFullTag("Supercalifragilisticexpialidocious").Result;

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetToursByTag()
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
            var actual = UnitOfWork.Tour.GetByTag("CaliFRAGIListIcexpIaLidoc").Result;

            //ASSERT
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetByTagOrName()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(tour => tour.Name
                                .Contains("Tour, Random But Unique",
                                    StringComparison.CurrentCultureIgnoreCase))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = UnitOfWork.Tour.GetByTagOrName("Tour, Random But Unique").Result;

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByCommerceName()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var firstCommerce = _databaseUtilities.Commerces.FirstOrDefault();

            var expected = _databaseUtilities.Tours
                            .Where(tour => tour.Nodes.Any(n => n.LocationId == firstCommerce.LocationId))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = UnitOfWork.Tour.GetByCommerceName(firstCommerce.Name).Result;

            //ASSERT
            Assert.Equal(expected, actual);
        }



        // Dispose pattern 
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
        }

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
