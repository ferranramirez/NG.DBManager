using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.Utilities;
using System;
using System.Collections.Generic;
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
        public void GetWithDealTypes()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours
                 .FirstOrDefault(t => t.Nodes.Any(n => n.Deal?.DealType != null));

            //ACT
            var actual = UnitOfWork.Tour.GetWithDealTypes(expected.Id);

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]//(Skip = "Doesn't work at the moment")]
        public async void GetAllWithDealTypes()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var expected = _databaseUtilities.Tours;

            //ACT
            var actual = await UnitOfWork.Tour.GetAllWithDealTypes();
            var actual2 = await UnitOfWork.Tour.GetAllWithDealTypes(0, 1);
            var actual22 = await UnitOfWork.Tour.GetAllWithDealTypes(1, 1);
            var actual23 = await UnitOfWork.Tour.GetAllWithDealTypes(2, 1);
            var actual3 = await UnitOfWork.Tour.GetAllWithDealTypes(2, 5);
            var actual4 = await UnitOfWork.Tour.GetAllWithDealTypes(1, 1);

            //ASSERT
            Assert.Equal(expected, actual);
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
        public void GetToursByTag()
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
            var actual = UnitOfWork.Tour.GetByTag("Supercalifragilisticexpialidocious").Result.ToList();

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetNoToursByPartialTag()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            //var expected = _databaseUtilities.Tours
            //                .Where(t => t.TourTags
            //                    .Any(tt => tt.Tag.Name
            //                        .Contains("CaliFRAGIListIcexpIaLidoc",
            //                            StringComparison.CurrentCultureIgnoreCase)))
            //                .OrderBy(t => t.Name)
            //                .ToList();

            //ACT
            var actual = UnitOfWork.Tour.GetByTag("CaliFRAGIListIcexpIaLidoc").Result.ToList();

            //ASSERT
            Assert.Empty(actual);
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
            var actual = UnitOfWork.Tour.GetByTagOrName("Tour, Random But Unique").Result.ToList();

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

        [Fact]
        public async Task GetByDealTypeAsync()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var dealName = _databaseUtilities.DealTypes.FirstOrDefault().Name;

            var dealTypeIds = _databaseUtilities.DealTypes
                .Where(dt => dt.Name.Contains(dealName))
                .Select(dt => dt.Id)
                .ToList();

            var expected = _databaseUtilities.Tours
                .Where(tour => tour.Nodes.Any(node => node.Deal != null &&
                    dealTypeIds.Contains(node.Deal.DealTypeId != null ? (Guid)node.Deal.DealTypeId : Guid.Empty)))
                .OrderBy(t => t.Name)
                .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetByDealType(dealName);

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetByEverythingAsync()
        {
            //ARRANGE
            _databaseUtilities.RandomSeed(Context);

            var filter = _databaseUtilities.Commerces.FirstOrDefault().Name;

            //var dealTypeIds = _databaseUtilities.DealTypes
            //    .Where(dt => dt.Name.Contains(dealName))
            //    .Select(dt => dt.Id)
            //    .ToList();
            //var toursByDealType = _databaseUtilities.Tours
            //    .Where(tour => tour.Nodes.Any(node => node.Deal != null &&
            //        dealTypeIds.Contains(node.Deal.DealTypeId != null ? (Guid)node.Deal.DealTypeId : Guid.Empty)))
            //    .ToList();

            //var firstCommerce = _databaseUtilities.Commerces.FirstOrDefault();
            //var toursByCommerceLocation = _databaseUtilities.Tours
            //                .Where(tour => tour.Nodes.Any(n => n.LocationId == firstCommerce.LocationId))
            //                .ToList();

            //var toursByTagOrName = _databaseUtilities.Tours
            //                .Where(tour => tour.Name
            //                    .Contains("Tour, Random But Unique",
            //                        StringComparison.CurrentCultureIgnoreCase))
            //                .ToList();


            var toursByDealType = await UnitOfWork.Tour.GetByDealType(filter);
            var toursByCommerceLocation = await UnitOfWork.Tour.GetByCommerceName(filter);
            var toursByTagOrName = await UnitOfWork.Tour.GetByTagOrName(filter);

            var expected = new List<Tour>();
            expected.AddRange(toursByDealType);
            expected.AddRange(toursByCommerceLocation);
            expected.AddRange(toursByTagOrName);

            //ACT
            var actual = await UnitOfWork.Tour.GetByEverything(filter);

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
