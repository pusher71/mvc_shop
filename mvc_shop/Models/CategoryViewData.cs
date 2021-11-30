using System.Collections.Generic;

namespace mvc_shop.Models
{
    public class CategoryViewData
    {
        public int categoryId; //текущая категория
        public ICollection<Product> productList; //список товаров в ней
        public int itemId; //то, что поиск нашёл
    }
}
