using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mvc_shop.Models;

namespace mvc_shop
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DataContext()
        {
            _ = Categories.ToList();
            _ = Products.ToList();
            _ = Comments.ToList();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            _ = Categories.ToList();
            _ = Products.ToList();
            _ = Comments.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mvc_shop;Username=postgres;Password=1234");
        }
    }
}
