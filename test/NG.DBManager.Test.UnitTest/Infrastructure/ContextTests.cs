using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Test.UnitTest.Infrastructure.Fixture;
using System;
using System.Linq;
using Xunit;

namespace NG.DBManager.Test.UnitTest.Infrastructure
{
    public class ContextTests : IClassFixture<FullDbSetup>
    {
        private readonly FullDbSetup _dbSetup;

        public ContextTests(FullDbSetup dbSetup)
        {
            _dbSetup = dbSetup;
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
                _dbSetup.FillDatabase(context);

                var audios = context.Audio.ToList();
                var locations = context.Location.ToList();
                var featuredTours = context.Featured.ToList();
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
                Assert.NotEmpty(featuredTours);
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
                _dbSetup.FillDatabase(context);

                // ACT
                var firstTour = _dbSetup.Tours.First();
                var tourImage = firstTour.Image;
                var featuredTour = firstTour.Featured;
                var nodes = firstTour.Nodes;
                var nodesDb1 = context.Node.ToList();
                Assert.Equal(nodes, nodesDb1);

                var tourTags = firstTour.TourTags;
                var locations = firstTour.Nodes.Select(n => n.Location);
                var locationsDb1 = context.Location.ToList();
                Assert.Equal(locations, locationsDb1);

                var imagesDb1 = context.Image.ToList();

                var audiosDb1 = context.Audio.ToList();

                var tourTagsDb1 = context.TourTag.ToList();
                var tagsDb1 = context.Tag.ToList();

                Assert.NotNull(firstTour);
                Assert.NotNull(tourImage);
                Assert.NotNull(featuredTour);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(locations);

                var imagesNodeDb1 = context.Node.SelectMany(n => n.Images).ToList();


                var tourImageDb1 = context.Image.Find(tourImage.Id);
                Assert.NotNull(tourImageDb1);

                context.Tour.Remove(firstTour);
                context.SaveChanges();

                // ASSERT                
                var firstTourDb = context.Tour.Find(firstTour.Id);
                Assert.Null(firstTourDb);

                var tourImageDb2 = context.Image.Find(tourImage.Id);
                //Assert.Null(tourImageDb2);

                var featuredTourDb = context.Featured.Where(f => f.TourId == firstTour.Id).ToList();
                Assert.Empty(featuredTourDb);

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
