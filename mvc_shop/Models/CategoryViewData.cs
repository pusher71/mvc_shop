using System.Collections.Generic;

namespace mvc_shop.Models
{
    public class CategoryViewData
    {
        public int categoryId;
        public ICollection<Product> productList;
    }
}
