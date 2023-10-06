using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Serie> Series { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<Serie>().ToTable("Series");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<Serie>().HasKey(serie => serie.Id);
            modelBuilder.Entity<Genre>().HasKey(genre => genre.Id);
            modelBuilder.Entity<Producer>().HasKey(producer => producer.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Producer>()
            .HasMany<Serie>(p => p.Series)
            .WithOne(s => s.Producer)
            .HasForeignKey(s => s.ProducerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genre>()
            .HasMany<Serie>(p => p.Series)
            .WithOne(s => s.Genre)
            .HasForeignKey(s => s.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }

    }
}
