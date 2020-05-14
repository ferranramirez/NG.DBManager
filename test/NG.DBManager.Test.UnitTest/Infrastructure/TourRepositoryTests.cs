using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.DBManager.Test.UnitTest.Infrastructure.Fixture;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NG.DBManager.Test.UnitTest.Infrastructure
{
    public class TourRepositoryTests
        : IClassFixture<FullDbSetup>
    {
        private readonly FullDbSetup _dbSetup;

        private const string partialTag = "sPOr";
        private const string fullTag = "Sport";

        private readonly NgContext Context;
        private readonly IUnitOfWork unitOfWork;


        public TourRepositoryTests(FullDbSetup dbSetup)
        {
            _dbSetup = dbSetup;

            Context = _dbSetup.GenerateInMemoryContext();
            unitOfWork = new UnitOfWork(Context);
        }


        [Fact]
        public void AddTour()
        {
            var t = unitOfWork.Tour;
            //ARRANGE
            Guid newTourId = Guid.NewGuid();
            Tour newTour = new Tour
            {
                Id = newTourId,
                Name = "My add test Tour",
                Description = "A nice description for such an interesting Tour",
            };

            //ACT
            unitOfWork.Tour.AddRange(_dbSetup.Tours);
            unitOfWork.Commit();

            //ACT
            unitOfWork.Tour.Add(newTour);

            //ASSERT
            var tourFromDb = unitOfWork.Tour.Get(newTourId);
            Assert.NotNull(tourFromDb);
            Assert.Equal(tourFromDb, newTour);


            var options = new DbContextOptionsBuilder<DBManager.Infrastructure.Contracts.Contexts.NgContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            using var context = new DBManager.Infrastructure.Contracts.Contexts.NgContext(options);
            var createdProperty = context.Entry(tourFromDb).Property("Created").CurrentValue;
            Assert.NotNull(createdProperty);

        }

        [Fact]
        public async Task GetToursByTag()
        {
            //ARRANGE
            _dbSetup.FillDatabase(Context);

            var expected = _dbSetup.TourTags
                .Where(tt => tt.Tag.Name
                    .Contains(partialTag, StringComparison.InvariantCultureIgnoreCase))
                .Select(tt => tt.Tour.Id);

            //ACT
            var actual = await unitOfWork.Tour.GetToursByTag(partialTag);

            //ASSERT
            Assert.Equal(expected, actual.Select(t => t.Id));
        }

        [Fact]
        public async Task GetToursByFullTagAsync()
        {
            //ARRANGE
            _dbSetup.FillDatabase(Context);

            var expected = _dbSetup.TourTags
                .Where(tt => tt.Tag.Name
                .Equals(fullTag, StringComparison.InvariantCultureIgnoreCase))
                .Select(tt => tt.Tour.Id);

            var actual = await unitOfWork.Tour.GetToursByFullTag(fullTag);

            //ASSERT
            Assert.Equal(expected, actual.Select(t => t.Id));
        }

        [Fact]
        public async Task GetAllFeaturedTours()
        {
            //ARRANGE
            _dbSetup.FillDatabase(Context);

            var expected = _dbSetup.Tours
                .Where(t => t.Featured != null)
                .Select(t => t.Id);

            //ACT
            var actual = await unitOfWork.Tour.GetFeaturedTours();

            //ASSERT
            Assert.Equal(expected, actual.Select(t => t.Id));
        }
    }
}
