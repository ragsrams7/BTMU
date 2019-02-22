using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DALCore;
using DALCore.Models;

namespace LivePoints.Repository
{
    public class LivePointsDBContext : DbContext
    {
        public LivePointsDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopUserMapping> ShopUserMappings { get; set; }
        public DbSet<Transactions> Transactionss { get; set; }
        public DbSet<ClaimHistory> ClaimHistorys { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShopAd> ShopAds { get; set; }
        public object SchemaName { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: DBGlobals.SchemaName);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Driver>().ToTable("Driver");
            modelBuilder.Entity<Shop>().ToTable("Shop");
            modelBuilder.Entity<ShopUserMapping>().ToTable("ShopUserMapping");
            modelBuilder.Entity<Transactions>().ToTable("Transactions");
            modelBuilder.Entity<ClaimHistory>().ToTable("ClaimHistory");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<Review>().ToTable("Review");
            modelBuilder.Entity<ShopAd>().ToTable("ShopAd");

            base.OnModelCreating(modelBuilder);
            //builder.Entity<User>()
            //    .HasOne(b => b.User)
            //    .WithMany(p => p.Posts)
            //    .IsRequired();
            //base.OnModelCreating(builder);
        }

    }
}
