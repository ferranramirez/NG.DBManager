using FizzWare.NBuilder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using NG.DBManager.Test.Utilities.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NG.DBManager.Test.Utilities
{
    public class DatabaseUtilities
    {
        public List<Image> Images;
        public List<Tag> Tags;
        public List<Deal> Deals;
        public List<DealType> DealTypes;
        public List<Tour> Tours;
        public List<Location> Locations;
        public List<Commerce> Commerces;
        public List<Restaurant> Restaurants;
        public List<Node> Nodes;
        public List<Review> Reviews;
        public List<User> Users;
        public List<Coupon> Coupons;

        private IList<Location> CommerceLocations;
        private List<Location> UsedCommerceLocation;
        private Tag TagWithManyTours;
        private string FullTagName = "Supercalifragilisticexpialidocious";
        private const string TourExistingName = "Custom Tour, Random But Unique Name";
        private readonly string CommonPassword = "10000.3ETjO6DdE/yDNjDOmPC4Xw==.EROLcKmnMnnl7k8GaBN2NukE5+ClhMJa9nh+DcbtGM0=";
        Hashtable ReviewUserCheck;
        public DatabaseUtilities()
        {
            GenerateData();
        }

        public void Reset(NgContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public int RandomSeed(NgContext context)
        {
            context.Image.AddRange(Images);
            context.User.AddRange(Users);
            context.Tag.AddRange(Tags);
            context.Location.AddRange(Locations);
            context.Restaurant.AddRange(Restaurants);
            context.Tour.AddRange(Tours);
            context.Coupon.AddRange(Coupons);
            context.Review.AddRange(Reviews);

            return context.SaveChanges();
        }

        #region Context generator
        public NgContext GenerateInMemoryContext()
        {
            var builder = new DbContextOptionsBuilder<NgContext>();
            builder.UseInMemoryDatabase("InMemoryDb");

            return new NgContext(builder.Options);
        }

        public NgContext GeneratePostgreSqlContext()
        {
            var builder = new DbContextOptionsBuilder<NgContext>();
            builder.UseNpgsql(DbTestResources.CONNECTIONSTRING);

            return new NgContext(builder.Options);
        }

        public NgContext GenerateSQLiteContext()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<NgContext>()
                .UseSqlite(connection).Options;

            return new NgContext(option);
        }
        #endregion Context generator

        #region Data generator
        // All the data is generated in the order it should be added into the DB, but the Images list
        private void GenerateData()
        {
            //Images has to be inserted first to allow the Users, Tours and Nodes be inserted
            Images = new List<Image>();

            Users = Builder<User>
                .CreateListOfSize(100)
                .All()
                    .With(u => u.Id = Guid.NewGuid())
                    .With(u => u.Name = Faker.Name.First())
                    .With(u => u.Birthdate = Faker.Identification.DateOfBirth())
                    .With(u => u.Email = Faker.Internet.Email())
                    .With(u => u.PhoneNumber = Faker.Phone.Number())
                    .With(u => u.Password = CommonPassword)
                    .With(u => u.Role = Role.Basic)
                    .With(u => u.Image = GenerateImages(1).First())
                .Random(40)
                    .With(u => u.Role = Role.Premium)
                .Random(20)
                    .With(u => u.Role = Role.Standard)
                .Random(5)
                    .With(u => u.Role = Role.Admin)
                .Random(1)
                    .With(u => u.Role = Role.Commerce)
                .Build()
                .ToList();

            Locations = Builder<Location>
                .CreateListOfSize(45)
                .All()
                    .With(l => l.Id = Guid.NewGuid())
                    .With(l => l.Name = Faker.Address.StreetName())
                    .With(l => l.Latitude = decimal.Parse(Faker.RandomNumber.Next(-9999, 9999).ToString()))
                    .With(l => l.Longitude = decimal.Parse(Faker.RandomNumber.Next(-9999, 9999).ToString()))
                .Build()
                .ToList();

            CommerceLocations = Builder<Location>
                .CreateListOfSize(45)
                .All()
                    .With(l => l.Id = Guid.NewGuid())
                    .With(l => l.Name = Faker.Address.StreetName())
                    .With(l => l.Latitude = decimal.Parse(Faker.RandomNumber.Next(-9999, 9999).ToString()))
                    .With(l => l.Longitude = decimal.Parse(Faker.RandomNumber.Next(-9999, 9999).ToString()))
                .Build()
                .ToList();


            DealTypes = Builder<DealType>
                .CreateListOfSize(2)
                .All()
                    .With(dt => dt.Id = Guid.NewGuid())
                    .With(dt => dt.Name = Faker.Internet.UserName())
                .Build()
                .ToList();

            Deals = Builder<Deal>
                .CreateListOfSize(20)
                .All()
                    .With(d => d.Id = Guid.NewGuid())
                    .With(d => d.Name = Faker.Lorem.Sentence(1))
                    .With(d => d.DealType = DealTypes.FirstOrDefault())
                    .With(d => d.DealTypeId = d.DealType.Id)
                .Random(2)
                    .With(d => d.DealType = DealTypes.LastOrDefault())
                    .With(d => d.DealTypeId = d.DealType.Id)
                .Build()
                .ToList();


            Nodes = new List<Node>();

            Tags = new List<Tag>();

            Tags = Builder<Tag>
                .CreateListOfSize(49)
                .All()
                    .With(tag => tag.Id = Guid.NewGuid())
                // .With(tag => tag.Name = Faker.Currency.Name())
                .Random(1)
                    .With(tag => tag.Id = Guid.NewGuid())
                    .With(tag => tag.Name = FullTagName)
                .Build()
                .ToList();

            TagWithManyTours = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = FullTagName,
            };

            // There are many (30) Tours that contain this tag
            Tags.Add(TagWithManyTours);

            // Commerces will never be directly added by the context since their hierarchy types will do it for them
            Commerces = new List<Commerce>();

            Restaurants = Builder<Restaurant>
                .CreateListOfSize(45)
                .All()
                    .With(r => r.Commerce = GenerateACommerce())
                    .With(r => r.CommerceId = r.Commerce.Id)
                .Build()
                .ToList();

            UsedCommerceLocation = new List<Location>();

            //CommerceLocations = Commerces.Select(com => com.Location).ToList();

            Tours = Builder<Tour>
                .CreateListOfSize(45)
                    .All()
                        .With(tour => tour.Id = Guid.NewGuid())
                        .With(tour => tour.Name = LimitMaxLength(Faker.Company.CatchPhrase(), 50))
                        .With(tour => tour.Description = Faker.Lorem.Paragraph())
                        .With(tour => tour.Duration = Faker.RandomNumber.Next())
                        .With(tour => tour.IsPremium = Faker.Boolean.Random())
                        .With(tour => tour.IsFeatured = false)
                        .With(tour => tour.IsActive = true)
                        .With(tour => tour.TourTags = AttachToGeneratedTag(tour))
                        .With(tour => tour.Nodes = GenerateNodes(tour))
                        .With(tour => tour.ImageId = GenerateImages(1).First().Id)
                    .Random(10)
                        .With(tour => tour.IsFeatured = true)
                    .Random(20)
                        .With(tour => tour.TourTags = AttachToTag(tour))
                    .Random(1)
                        .With(tour => tour.Name = TourExistingName)
                    .Build()
                    .ToList();

            Coupons = Builder<Coupon>
                .CreateListOfSize(50)
                    .All()
                        .With(c => c.Id = Guid.NewGuid())
                        .With(c => c.User = Pick<User>.RandomItemFrom(Users))
                        .With(c => c.UserId = c.User.Id)
                        .With(c => c.Node = SelectCommerceNode())
                        .With(c => c.NodeId = c.Node.Id)
                        .With(c => c.ValidationDate = default)
                        .With(c => c.Content = Faker.Lorem.Sentence(10))
                    .Random(25)
                        .With(c => c.ValidationDate = new DateTime())
                    .Build()
                    .ToList();

            Reviews = new List<Review>();
            // HashTable to avoid creating repeated Reviews in AddTourNotReviewedByUser(Review)
            ReviewUserCheck = new Hashtable();

            Reviews = Builder<Review>
                .CreateListOfSize(10)
                    .All()
                        .With(r => r.User = Pick<User>.RandomItemFrom(Users))
                        .With(r => r.UserId = r.User.Id)
                        .With(r => r.Tour = AddTourNotReviewedByUser(r))
                        .With(r => r.TourId = r.Tour.Id)
                        .With(r => r.Score = 5)
                    .Random(2)
                        .With(r => r.Score = 4)
                    .Random(1)
                        .With(r => r.Score = 3)
                    .Random(1)
                        .With(r => r.Score = 2)
                    .Random(1)
                        .With(r => r.Score = 1)
                    .Build()
                    .ToList();
        }

        private Node SelectCommerceNode()
        {
            var location = Pick<Location>.RandomItemFrom(CommerceLocations);
            var commerceLocationId = location.Id;
            return Nodes.First(n => n.LocationId == commerceLocationId);
        }

        private static string LimitMaxLength(string str, int maxLength)
        {
            return new string(str.Take(maxLength).ToArray());
        }

        private Tour AddTourNotReviewedByUser(Review review)
        {
            var isReviewed = true;
            var tour = new Tour();

            while (isReviewed)
            {
                tour = Pick<Tour>.RandomItemFrom(Tours);
                isReviewed = ReviewUserCheck.ContainsKey(tour.Id);
            }

            ReviewUserCheck.Add(tour.Id, review.UserId);

            return tour;
        }

        private IList<Image> GenerateImages(int numOfElements)
        {
            var generatedImages = Builder<Image>
                .CreateListOfSize(numOfElements)
                .All()
                    .With(i => i.Id = Guid.NewGuid())
                    .With(i => i.Name = string.Concat(Faker.Address.City(), ".jpg"))
                .Build()
                .ToList();

            Images.AddRange(generatedImages);

            return generatedImages;
        }

        private Commerce GenerateACommerce()
        {
            List<Location> nonRepeatedLocations = CommerceLocations.Except(Commerces.Select(c => c.Location)).ToList();

            var generatedCommerce = Builder<Commerce>
                .CreateNew()
                .With(c => c.Id = Guid.NewGuid())
                .With(c => c.Name = LimitMaxLength(Faker.Finance.Isin(), 80))
                .With(c => c.Location = Pick<Location>.RandomItemFrom(nonRepeatedLocations))
                .With(c => c.LocationId = c.Location.Id)
                .With(c => c.User = GenerateCommerceUser(c))
                .With(c => c.UserId = c.User.Id)
                .With(c => c.CommerceDeals = AttatchToSomeDeals(c))
                .Build();

            Commerces.Add(generatedCommerce);

            return generatedCommerce;
        }

        private List<CommerceDeal> AttatchToSomeDeals(Commerce commerce)
        {
            return Builder<CommerceDeal>
                .CreateListOfSize(1) // Number of Deals for each Commerce
                .All()
                    .With(comDeal => comDeal.Deal = Pick<Deal>.RandomItemFrom(Deals))
                    .With(comDeal => comDeal.Commerce = commerce)
                .Build()
                .ToList();
        }

        private User GenerateCommerceUser(Commerce commerce)
        {
            var generatedUser = Builder<User>
                .CreateNew()
                .With(u => u.Id = Guid.NewGuid())
                .With(u => u.Name = Faker.Name.First())
                .With(u => u.Birthdate = Faker.Identification.DateOfBirth())
                .With(u => u.Email = Faker.Internet.Email())
                .With(u => u.PhoneNumber = Faker.Phone.Number())
                .With(u => u.Password = CommonPassword)
                .With(u => u.Role = Role.Commerce)
                .With(u => u.Commerce = commerce)
                .With(u => u.Image = GenerateImages(1).First())
                .Build();

            Users.Add(generatedUser);

            return generatedUser;
        }

        private IList<TourTag> AttachToTag(Tour tour)
        {
            return Builder<TourTag>
                .CreateListOfSize(1) // Number of Tags for each Tour
                .All()
                    .With(tourTag => tourTag.Tag = TagWithManyTours)
                    .With(tourTag => tourTag.Tour = tour)
                .Build()
                .ToList();
        }

        private IList<TourTag> AttachToGeneratedTag(Tour tour)
        {
            return Builder<TourTag>
                .CreateListOfSize(1) // Number of Tags for each Tour
                .All()
                    .With(tourTag => tourTag.Tag = GenerateTag())
                    .With(tourTag => tourTag.Tour = tour)
                .Build()
                .ToList();
        }
        private Tag GenerateTag()
        {
            var generatedTag = Builder<Tag>
                .CreateNew()
                .With(tag => tag.Id = Guid.NewGuid())
                .With(tag => tag.Name = Faker.Currency.ThreeLetterCode())
                .Build();

            Tags.Add(generatedTag);

            return generatedTag;
        }

        private IList<Node> GenerateNodes(Tour tour)
        {
            var generatedNodes = Builder<Node>
                .CreateListOfSize(5) // Number of Nodes for each Tour
                .All()
                    .With(node => node.Id = Guid.NewGuid())
                    .With(node => node.Name = LimitMaxLength(Faker.Company.BS(), 50))
                    .With(node => node.Order = Faker.RandomNumber.Next())
                    .With(node => node.Description = Faker.Lorem.Paragraph())
                    .With(node => node.Images = Pick<Image>.UniqueRandomList(
                        With.Between(5).And(10).Elements).From(Images))
                    .With(node => node.Location = Pick<Location>.RandomItemFrom(Locations.Except(CommerceLocations).ToList()))
                    .With(node => node.LocationId = node.Location.Id)
                    .With(node => node.Audios = GenerateNodeAudios(node))
                    .With(node => node.TourId = tour.Id)
                    .With(node => node.Images = GenerateImages(3))
                    .With(node => node.DealId = null)
                .Random(1)
                    .With(node => node.Location = SelectCommerceLocation())
                    .With(node => node.LocationId = node.Location.Id)
                    .With(node => node.Deal = SelectDeal(node))
                    .With(node => node.DealId = node.Deal.Id)
                .Build()
                .ToList();

            Nodes.AddRange(generatedNodes);

            return generatedNodes;
        }

        private Location SelectCommerceLocation()
        {
            var commerceLocation = Pick<Location>.RandomItemFrom(CommerceLocations.Except(UsedCommerceLocation).ToList());
            UsedCommerceLocation.Add(commerceLocation);
            return commerceLocation;
        }

        private Deal SelectDeal(Node node)
        {
            var commerce = Commerces
                .First(c => c.LocationId == node.LocationId);

            var deal = commerce.CommerceDeals
                .Select(cd => cd.Deal).First();

            return deal;
        }

        private IList<Audio> GenerateNodeAudios(Node node)
        {
            return Builder<Audio>
                .CreateListOfSize(4) // Number of Audios for each Node
                .All()
                    .With(a => a.Id = Guid.NewGuid())
                    .With(a => a.Name = LimitMaxLength(Faker.Address.Country(), 20))
                    .With(a => a.NodeId = node.Id)
                .Build()
                .ToList();
        }
        #endregion Data generator
    }
}