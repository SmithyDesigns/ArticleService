using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Article_Service.src.Domain.Entities;
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
        public DbSet<Customer> Customers { get; set; }

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
                .Property(a => a.Description)
                .HasMaxLength(255)
                .IsRequired();

            // Customer 
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Username)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Password)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}