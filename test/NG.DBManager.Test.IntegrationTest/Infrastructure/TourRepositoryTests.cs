using Microsoft.Extensions.DependencyInjection;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Test.IntegrationTest.Fixture;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NG.DBManager.Test.IntegrationTest.Infrastructure.Fixture
{
    public class TourRepositoryTests
        : IClassFixture<IoCModuleFixture>, IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        private readonly NgContext Context;
        private readonly IUnitOfWork UnitOfWork;

        public TourRepositoryTests(IoCModuleFixture ioCModule, DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;

            var serviceProvider = ioCModule._serviceProvider;

            Context = serviceProvider.GetService<NgContext>();
            UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
        }


        [Fact]
        public void AddTour()
        {
            //ARRANGE
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            Guid newTourId = Guid.NewGuid();
            Tour newTour = new Tour
            {
                Id = newTourId,
                Name = "My add test Tour",
                Description = "A nice description for such an interesting Tour",
            };

            //ACT
            UnitOfWork.Tour.Add(newTour);

            //ASSERT
            var tourFromDb = UnitOfWork.Tour.Get(newTourId);
            Assert.NotNull(tourFromDb);
            Assert.Equal(tourFromDb, newTour);

            var createdProperty = Context.Entry(tourFromDb).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);
        }

        [Fact]
        public async Task GetAllFeaturedTours()
        {
            //ARRANGE
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            _databaseUtilities.Seed(Context);
            var expected = _databaseUtilities.Tours
                            .Where(t => t.IsFeatured)
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetFeatured();

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetLastOnesCreated()
        {
            //ARRANGE
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            _databaseUtilities.Seed(Context);

            //ACT
            var actual = await UnitOfWork.Tour.GetLastOnesCreated(1);


            //ASSERT
            var lastTourCreated = actual.Single();
            Assert.NotNull(lastTourCreated);

            var createdProperty = Context.Entry(lastTourCreated).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);
        }

        [Fact]
        public async Task GetToursByFullTagAsync()
        {
            //ARRANGE
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            _databaseUtilities.Seed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(t => t.TourTags
                                .Any(tt => tt.Tag.Name
                                    .Equals("Supercalifragilisticexpialidocious",
                                        StringComparison.CurrentCultureIgnoreCase)))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetByFullTag("Supercalifragilisticexpialidocious");

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetToursByTag()
        {
            //ARRANGE
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            _databaseUtilities.Seed(Context);

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
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task GetByTagOrName()
        {
            //ARRANGE
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            _databaseUtilities.Seed(Context);

            var expected = _databaseUtilities.Tours
                            .Where(tour => tour.Name
                                .Contains("Tour, Random But Unique",
                                    StringComparison.CurrentCultureIgnoreCase))
                            .OrderBy(t => t.Name)
                            .ToList();

            //ACT
            var actual = await UnitOfWork.Tour.GetByTagOrName("Tour, Random But Unique");

            //ASSERT
            Assert.Equal(expected, actual);
        }
    }
}
