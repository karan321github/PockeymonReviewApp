
using Microsoft.EntityFrameworkCore;
using PockeymonReviewApp.Models;

namespace PockeymonReviewApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pockymon> Pockeymon { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewer { get; set; }
        public DbSet<PockeymonCategory> PockeymonCategories { get; set; }
        public DbSet<PockeymonOwner> PockeymonOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary key for PockeymonCategory
            modelBuilder.Entity<PockeymonCategory>()
                .HasKey(pc => new { pc.PockeymonId, pc.CategoryId });

            // Define relationship to Pockeymon
            modelBuilder.Entity<PockeymonCategory>()
                .HasOne(pc => pc.Pockymon)
                .WithMany(p => p.PockeymonCategories)
                .HasForeignKey(pc => pc.PockeymonId); // Corrected to PockeymonId

            // Define relationship to Category
            modelBuilder.Entity<PockeymonCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PockeymonCategories)
                .HasForeignKey(pc => pc.CategoryId); // This part is correct


            modelBuilder.Entity<PockeymonOwner>()
           .HasKey(po => new { po.PockeymonId, po.OwnerId });

            modelBuilder.Entity<PockeymonOwner>()
                .HasOne(po => po.Pockymon)
                .WithMany(p => p.PockeymonOwners)
                .HasForeignKey(po => po.PockeymonId); // Correct

            modelBuilder.Entity<PockeymonOwner>()
                .HasOne(po => po.Owner)
                .WithMany(o => o.PockeymonOwners)
                .HasForeignKey(po => po.OwnerId); // Correct

        }
    }
    }
