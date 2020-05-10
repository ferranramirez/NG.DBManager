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
                _dbSetup.FillDb(context);

                var audios = context.Audio.ToList();
                var audioImages = context.AudioImage.ToList();
                var coordinates = context.Coordinates.ToList();
                var featuredTours = context.Featured.ToList();
                var images = context.Image.ToList();
                var nodes = context.Node.ToList();
                var nodeAudios = context.NodeAudio.ToList();
                var nodeImages = context.NodeImage.ToList();
                var restaurants = context.Restaurant.ToList();
                var reviews = context.Review.ToList();
                var tags = context.Tag.ToList();
                var tours = context.Tour.ToList();
                var tourTags = context.TourTag.ToList();
                var users = context.User.ToList();

                // ASSERT
                Assert.NotEmpty(audios);
                Assert.NotEmpty(audioImages);
                Assert.NotEmpty(coordinates);
                Assert.NotEmpty(featuredTours);
                Assert.NotEmpty(images);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(nodeAudios);
                Assert.NotEmpty(nodeImages);
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
                _dbSetup.FillDb(context);

                // ACT
                var firstTour = _dbSetup.Tours.First();

                context.Tour.Remove(firstTour);
                context.SaveChanges();

                // ASSERT                
                var firstTourDb = context.Tour.Find(firstTour.Id);
                Assert.Null(firstTourDb);
            }
        }


        [Fact(Skip = "Not Implemented Yet")]
        public void DeleteUser_DeletesUserAndImage_DoesNotDeleteReview()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<NgContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NgContext(options))
            {
                // ACT
                _dbSetup.FillDb(context);

                var audios = context.Audio.ToList();
                var audioImages = context.AudioImage.ToList();
                var coordinates = context.Coordinates.ToList();
                var featuredTours = context.Featured.ToList();
                var images = context.Image.ToList();
                var nodes = context.Node.ToList();
                var nodeAudios = context.NodeAudio.ToList();
                var nodeImages = context.NodeImage.ToList();
                var restaurants = context.Restaurant.ToList();
                var reviews = context.Review.ToList();
                var tags = context.Tag.ToList();
                var tours = context.Tour.ToList();
                var tourTags = context.TourTag.ToList();
                var users = context.User.ToList();

                // ASSERT
                Assert.NotEmpty(audios);
                Assert.NotEmpty(audioImages);
                Assert.NotEmpty(coordinates);
                Assert.NotEmpty(featuredTours);
                Assert.NotEmpty(images);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(nodeAudios);
                Assert.NotEmpty(nodeImages);
                Assert.NotEmpty(restaurants);
                Assert.NotEmpty(reviews);
                Assert.NotEmpty(tags);
                Assert.NotEmpty(tours);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(users);

            }
        }

        [Fact(Skip = "Not Implemented Yet")]
        public void DeleteFeatured_DeletesFeatured()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<NgContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NgContext(options))
            {
                // ACT
                _dbSetup.FillDb(context);

                var audios = context.Audio.ToList();
                var audioImages = context.AudioImage.ToList();
                var coordinates = context.Coordinates.ToList();
                var featuredTours = context.Featured.ToList();
                var images = context.Image.ToList();
                var nodes = context.Node.ToList();
                var nodeAudios = context.NodeAudio.ToList();
                var nodeImages = context.NodeImage.ToList();
                var restaurants = context.Restaurant.ToList();
                var reviews = context.Review.ToList();
                var tags = context.Tag.ToList();
                var tours = context.Tour.ToList();
                var tourTags = context.TourTag.ToList();
                var users = context.User.ToList();

                // ASSERT
                Assert.NotEmpty(audios);
                Assert.NotEmpty(audioImages);
                Assert.NotEmpty(coordinates);
                Assert.NotEmpty(featuredTours);
                Assert.NotEmpty(images);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(nodeAudios);
                Assert.NotEmpty(nodeImages);
                Assert.NotEmpty(restaurants);
                Assert.NotEmpty(reviews);
                Assert.NotEmpty(tags);
                Assert.NotEmpty(tours);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(users);

            }
        }


        [Fact(Skip = "Not Implemented Yet")]
        public void DeleteAudio_DeletesAudio()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<NgContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NgContext(options))
            {
                // ACT
                _dbSetup.FillDb(context);

                var audios = context.Audio.ToList();
                var audioImages = context.AudioImage.ToList();
                var coordinates = context.Coordinates.ToList();
                var featuredTours = context.Featured.ToList();
                var images = context.Image.ToList();
                var nodes = context.Node.ToList();
                var nodeAudios = context.NodeAudio.ToList();
                var nodeImages = context.NodeImage.ToList();
                var restaurants = context.Restaurant.ToList();
                var reviews = context.Review.ToList();
                var tags = context.Tag.ToList();
                var tours = context.Tour.ToList();
                var tourTags = context.TourTag.ToList();
                var users = context.User.ToList();

                // ASSERT
                Assert.NotEmpty(audios);
                Assert.NotEmpty(audioImages);
                Assert.NotEmpty(coordinates);
                Assert.NotEmpty(featuredTours);
                Assert.NotEmpty(images);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(nodeAudios);
                Assert.NotEmpty(nodeImages);
                Assert.NotEmpty(restaurants);
                Assert.NotEmpty(reviews);
                Assert.NotEmpty(tags);
                Assert.NotEmpty(tours);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(users);

            }
        }

        [Fact(Skip = "Not Implemented Yet")]
        public void DeleteImage_DeletesImage()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<NgContext>()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new NgContext(options))
            {
                // ACT
                _dbSetup.FillDb(context);

                var audios = context.Audio.ToList();
                var audioImages = context.AudioImage.ToList();
                var coordinates = context.Coordinates.ToList();
                var featuredTours = context.Featured.ToList();
                var images = context.Image.ToList();
                var nodes = context.Node.ToList();
                var nodeAudios = context.NodeAudio.ToList();
                var nodeImages = context.NodeImage.ToList();
                var restaurants = context.Restaurant.ToList();
                var reviews = context.Review.ToList();
                var tags = context.Tag.ToList();
                var tours = context.Tour.ToList();
                var tourTags = context.TourTag.ToList();
                var users = context.User.ToList();

                // ASSERT
                Assert.NotEmpty(audios);
                Assert.NotEmpty(audioImages);
                Assert.NotEmpty(coordinates);
                Assert.NotEmpty(featuredTours);
                Assert.NotEmpty(images);
                Assert.NotEmpty(nodes);
                Assert.NotEmpty(nodeAudios);
                Assert.NotEmpty(nodeImages);
                Assert.NotEmpty(restaurants);
                Assert.NotEmpty(reviews);
                Assert.NotEmpty(tags);
                Assert.NotEmpty(tours);
                Assert.NotEmpty(tourTags);
                Assert.NotEmpty(users);

            }
        }

    }
}
