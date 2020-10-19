using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Models;

namespace NG.DBManager.Infrastructure.Contracts.Contexts
{
    public class NgContext : DbContext
    {
        public NgContext(DbContextOptions<NgContext> options)
            : base(options)
        {
        }

        public DbSet<Audio> Audio { get; set; }
        //public DbSet<AudioImage> AudioImage { get; set; }
        public DbSet<Commerce> Commerce { get; set; }
        public DbSet<CommerceDeal> CommerceDeal { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<DealType> DealType { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Node> Node { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<TourTag> TourTag { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AudioImage>().HasKey(ai => new { ai.AudioId, ai.ImageId });

            modelBuilder.Entity<CommerceDeal>().HasKey(cd => new { cd.CommerceId, cd.DealId });

            modelBuilder.Entity<Location>().HasIndex(l => new { l.Latitude, l.Longitude }).IsUnique();

            modelBuilder.Entity<Review>().HasKey(r => new { r.UserId, r.TourId });

            //modelBuilder.Entity<Tour>().Property<DateTime>("Created");

            modelBuilder.Entity<TourTag>().HasKey(tt => new { tt.TourId, tt.TagId });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.PhoneNumber).IsUnique();
            });

            modelBuilder.Entity<User>().HasData(DataUtils.UserSeed());
            modelBuilder.Entity<Location>().HasData(DataUtils.LocationSeed());
            modelBuilder.Entity<Commerce>().HasData(DataUtils.CommerceSeed());
            modelBuilder.Entity<Tour>().HasData(DataUtils.TourSeed());
            modelBuilder.Entity<Node>().HasData(DataUtils.NodeSeed());
        }
    }
}
