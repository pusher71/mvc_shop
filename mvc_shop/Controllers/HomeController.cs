using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc_shop.Models;

using Microsoft.AspNetCore.Authorization;

namespace mvc_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DbRepository _dbRepository;

        public HomeController(ILogger<HomeController> logger, DbRepository dbRepository)
        {
            _logger = logger;
            _dbRepository = dbRepository;
        }

        public IActionResult Index()
        {
            return View("CategoryList", _dbRepository.GetCategoryCollection());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BuyProduct()
        {
            return View("ForbiddenBuyProduct");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuyProduct(int productId, int quantity)
        {
            Product target;
            try
            {
                target = _dbRepository.GetProductById(productId);
                target.Buy(quantity);
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }

            _dbRepository.UpdateProduct(target);
            return View(target.Category.Id);
        }

        public IActionResult Category(int categoryId)
        {
            ICollection<Product> list;
            try
            {
                list = _dbRepository.GetCategoryById(categoryId).Products;
                //ICollection<Product> exp = _dbRepository.GetAllProducts();
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }

            return View("Category", new CategoryViewData() { categoryId = categoryId, productList = list });
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View("AddCategory");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            try
            {
                int newId = _dbRepository.GetCategoryCollection().OrderBy(c => c.Id).Last().Id + 1;
                Category newCategory = new Category(newId, name);
                _dbRepository.AddCategory(newCategory);
                return Index();
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [HttpGet]
        public IActionResult DeleteCategory(int categoryId)
        {
            return View("DeleteCategory", categoryId);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteCategory(int categoryId, bool confirm)
        {
            try
            {
                _dbRepository.DeleteCategory(_dbRepository.GetCategoryById(categoryId));
                return Index();
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        public IActionResult AddProduct(int categoryId)
        {
            return View("AddProduct", _dbRepository.GetCategoryById(categoryId).Name);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct(string categoryName, string name, string brend, float price, int quantity, float power, float mass, int speedCount)
        {
            try
            {
                Category target = _dbRepository.GetCategoryByName(categoryName);
                int newId = _dbRepository.GetAllProducts().OrderBy(p => p.Id).Last().Id + 1;
                _dbRepository.AddProduct(target, new Product(newId, name, brend, price, quantity, power, mass, speedCount));
                return Index();
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [HttpGet]
        public IActionResult DeleteProduct()
        {
            return View("ForbiddenDeleteProduct");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                _dbRepository.DeleteProduct(_dbRepository.GetProductById(productId));
                return Index();
            }
            catch (Exception e)
            {
                return View("ErrorPage", e.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
