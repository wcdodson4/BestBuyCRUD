using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace BestBuyCRUD
{
    public class DapperProductRepository : IProductRepository
    {
        public DapperProductRepository()
        {

        }

        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;").ToList();
        }

        public void CreateProduct(string newProductName, double newProductPrice, int newProductCategoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@productName, @productPrice, @productCategoryID);",
                new { productName = newProductName, productPrice = newProductPrice, productCategoryID = newProductCategoryID });
        }

        public void UpdateProduct(int productID, string newProductName, double newProductPrice)
        {
            _connection.Execute("UPDATE Products SET Name = @productName, Price = @productPrice WHERE ProductID = @oldProductID;",
                new { productName = newProductName, oldProductID = productID, productPrice = newProductPrice });
        }

        public void DeleteProduct(int productid)
        {
            _connection.Execute("DELETE FROM Reviews WHERE ProductID = @productID;", new { productID = productid});
            _connection.Execute("DELETE FROM Sales WHERE ProductID = @productID;", new { productID = productid });
            _connection.Execute("DELETE FROM Products WHERE ProductID = @productID;", new { productID = productid });
        }
    }
}
