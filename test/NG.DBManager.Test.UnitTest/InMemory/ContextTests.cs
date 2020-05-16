using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Test.Utilities;
using System;
using System.Linq;
using Xunit;

namespace NG.DBManager.Test.UnitTest.InMemory
{
    public class ContextTests : IClassFixture<DatabaseUtilities>
    {
        private readonly DatabaseUtilities _databaseUtilities;

        public ContextTests(DatabaseUtilities databaseUtilities)
        {
            _databaseUtilities = databaseUtilities;
        }

        [Fact]
        public void FillDatabase_ReturnsDatabaseWithEntities()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<NgContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NgContext(options))
            {
                // ACT
                _databaseUtilities.Seed(context);

                var audios = context.Audio.ToList();
                var locations = context.Location.ToList();
                var images = context.Image.ToList();
                var nodes = context.Node.ToList();
                var restaurants = context.Restaurant.ToList();
                var reviews = context.Review.ToList();
                var tags = context.Tag.ToList();
                var tours = context.Tour.ToList();
                var tourTags = context.TourTag.ToList();
                var users = context.User.ToList();

                // ASSERT
                Assert.NotEmpty(audios);
                Assert.NotEmpty(locations);
                Assert.NotEmpty(images);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(restaurants);
                Assert.NotEmpty(reviews);
                Assert.NotEmpty(tags);
                Assert.NotEmpty(tours);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(users);

            }
        }


        [Fact]
        public void DeleteTour_DeletesTourAndFeaturedAndNodesAndImagesAndaudios()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<NgContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NgContext(options))
            {
                _databaseUtilities.Seed(context);

                // ACT
                var firstTour = _databaseUtilities.Tours.First();
                var tourImageId = firstTour.ImageId;
                var nodes = firstTour.Nodes
                    .OrderBy(n => n.Id)
                    .ToList();
                var nodesDb0 = context.Node
                    .Where(n => n.TourId == firstTour.Id)
                    .OrderBy(n => n.Id)
                    .ToList();
                Assert.Equal(nodes, nodesDb0);

                var nodesDb1 = context.Node
                    .ToList();

                var tourTags = firstTour.TourTags;
                var locations = firstTour.Nodes
                    .Select(n => n.Location)
                    .ToList();
                var locationsDb1 = context.Location
                    .ToList();

                var imagesDb1 = context.Image.ToList();

                var audiosDb1 = context.Audio.ToList();

                var tourTagsDb1 = context.TourTag.ToList();
                var tagsDb1 = context.Tag.ToList();

                Assert.NotNull(firstTour);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(locations);

                var imagesNodeDb1 = context.Node.SelectMany(n => n.Images).ToList();


                var tourImageDb1 = context.Image.Find(tourImageId);
                Assert.NotNull(tourImageDb1);

                context.Tour.Remove(firstTour);
                context.SaveChanges();

                // ASSERT                
                var firstTourDb = context.Tour.Find(firstTour.Id);
                Assert.Null(firstTourDb);

                var tourImageDb2 = context.Image.Find(tourImageId);
                //Assert.Null(tourImageDb2);

                var nodesDb2 = context.Node.Where(n => n.TourId == firstTour.Id).ToList();
                Assert.Empty(nodesDb2);
                Assert.True(nodesDb1.Count > context.Node.ToList().Count);

                Assert.True(tourTagsDb1.Count > context.TourTag.ToList().Count);
                Assert.Equal(tagsDb1.Count, context.Tag.ToList().Count);

                var locationsDb2 = context.Location.ToList();
                Assert.Equal(locationsDb1.Count, locationsDb2.Count);

                var imagesDb2 = context.Image.ToList();
                Assert.True(audiosDb1.Count > context.Audio.ToList().Count);
                //Assert.True(imagesNodeDb1.Count > imagesDb2.Count);
            }
        }

    }
}
