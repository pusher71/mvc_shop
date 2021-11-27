using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mvc_shop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product GetProductById(int productId)
        {
            return Products.FirstOrDefault(p => p.Id == productId);
        }

        public void AddProduct(Product product)
        {
            if (Products.Contains(product))
                throw new Exception("Ошибка. Данный товар уже существует в данной категории");
            product.SetCategory(this);
            Products.Add(product);
        }
    }
}
