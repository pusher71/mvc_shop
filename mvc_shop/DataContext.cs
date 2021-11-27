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

        public DataContext()
        {
            _ = Categories.ToList();
            _ = Products.ToList();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            _ = Categories.ToList();
            _ = Products.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=jsnet.ru;Port=23112;Database=db7;Username=user7;Password=user7pass476");
        }
    }
}
