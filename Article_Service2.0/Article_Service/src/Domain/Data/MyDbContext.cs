using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Article>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Article>()
                .Property(a => a.Title)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(a => a.Html)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}