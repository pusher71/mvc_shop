using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace mvc_shop.Models
{
    public class Product
    {
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public void SetCategory(Category cat)
        {
            if (Category != null)
                throw new System.Exception("Ошибка. Категория товара уже задана");
            Category = cat;
            CategoryId = cat.Id;
        }

        public int Id { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }
        public string Brend { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }


        // Свойства дрелей
        public float Power { get; set; }
        public float Mass { get; set; }
        public int SpeedCount { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Product(int id, string name, string brend, float price, int quantity, float power, float mass, int speedCount)
        {
            Id = id;
            Name = name;
            Brend = brend;
            Price = price;
            Quantity = quantity;

            Power = power;
            Mass = mass;
            SpeedCount = speedCount;
        }

        public void Buy(int quantity)
        {
            if (Quantity < quantity)
                throw new System.Exception("Ошибка. Товар уже куплен");
            Quantity -= quantity;
        }

        public Comment GetCommentById(int commentId)
        {
            return Comments.FirstOrDefault(c => c.Id == commentId);
        }
    }
}
