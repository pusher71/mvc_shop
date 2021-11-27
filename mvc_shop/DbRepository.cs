using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mvc_shop.Models;

namespace mvc_shop
{
    public class DbRepository
    {
        private DataContext _context;

        public DbRepository(DataContext dataContext)
        {
            _context = dataContext;
            if (_context.Categories.Any()) return;

            // Добавление категорий
            Category drill = new Category(1, "Безударная дрель");
            Category hitDrill = new Category(2, "Ударная дрель");
            Category mixerDrill = new Category(3, "Дрель-Миксер");
            _context.Categories.AddRange(new List<Category> { drill, hitDrill, mixerDrill });

            // Добавление товаров в категории
            // Безударные дрели
            Product drill1 = new Product(1, "6413", "MAKITA", 3370, 1, 450, 1.3f, 1);
            Product drill2 = new Product(2, "DS-55", "OASIS", 2450, 1, 550, 1.5f, 2);
            Product drill3 = new Product(3, "Д-10", "ИНТЕРСКОЛ", 1690, 1, 420, 1.5f, 1);
            Product drill4 = new Product(4, "ДЭ-10", "СТАВР", 1700, 1, 650, 1.4f, 1);
            drill.AddProduct(drill1);
            drill.AddProduct(drill2);
            drill.AddProduct(drill3);
            drill.AddProduct(drill4);

            // Ударные дрели
            Product drill5 = new Product(5, "HP1640K", "MAKITA", 4670, 1, 680, 2, 1);
            Product drill6 = new Product(6, "ДУ-13", "ИНТЕРСКОЛ", 2590, 1, 780, 2.2f, 1);
            Product drill7 = new Product(7, "ДУ-22", "ИНТЕРСКОЛ", 6490, 1, 1200, 3.8f, 2);
            Product drill8 = new Product(8, "ДУ-850", "ВИХРЬ", 2590, 1, 850, 1.9f, 1);
            hitDrill.AddProduct(drill5);
            hitDrill.AddProduct(drill6);
            hitDrill.AddProduct(drill7);
            hitDrill.AddProduct(drill8);

            // Дрели-Миксеры
            Product drill9 = new Product(9, "ЗДМ-820", "ЗУБР", 5980, 1, 820, 3, 1);
            Product drill10 = new Product(10, "Д-1000М", "ВИХРЬ", 5990, 1, 1000, 3.3f, 1);
            Product drill11 = new Product(11, "MX-160", "OASIS", 6190, 1, 1600, 5.4f, 2);
            Product drill12 = new Product(12, "MP-1050-1", "ЗУБР", 6990, 1, 1050, 4.7f, 2);
            mixerDrill.AddProduct(drill9);
            mixerDrill.AddProduct(drill10);
            mixerDrill.AddProduct(drill11);
            mixerDrill.AddProduct(drill12);

            _context.Products.AddRange(new List<Product> { drill1, drill2, drill3, drill4, drill5, drill6, drill7, drill8, drill9, drill10, drill11, drill12 });
            _context.SaveChanges();
        }

        public List<Category> GetCategoryCollection()
        {
            return _context.Categories.Include(c => c.Products).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == categoryName);
        }

        public void AddCategory(Category category)
        {
            if (_context.Categories.Contains(category))
                throw new Exception("Ошибка. Данная категория уже существует");

            //проверка совпадения по имени
            foreach (Category c in _context.Categories)
                if (c.Name == category.Name)
                    throw new Exception("Ошибка. Категория с данным именем уже существует");

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            if (!_context.Categories.Contains(category))
                throw new Exception("Ошибка. Данная категория не существует");
            foreach (Product p in category.Products)
                _context.Products.Remove(p);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public void AddProduct(Category category, Product product)
        {
            if (_context.Products.Contains(product))
                throw new Exception("Ошибка. Данный товар уже существует");

            //проверка совпадения по имени
            foreach (Product p in _context.Products)
                if (p.Name == product.Name)
                    throw new Exception("Ошибка. Товар с данным именем уже существует");

            category.AddProduct(product);
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            product.Category.Products.Remove(product);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product GetProductById(int productId)
        {
            ICollection<Product> products = GetAllProducts();
            Product result = products.FirstOrDefault(p => p.Id == productId);

            if (result == null)
                throw new Exception("Ошибка. Товар с данным id не существует");

            return result;
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            //_context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
