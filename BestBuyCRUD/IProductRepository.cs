using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyCRUD
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();

        public void CreateProduct(string name, double price, int categoryid);
    }
}
