using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");
            var prodRepo = new DapperProductRepository(conn);
            //-------creating a new product------------------------
            Console.WriteLine("What is the new product's name?");
            var nameResponse = Console.ReadLine();
            Console.WriteLine("What is the new product's price?");
            var priceResponse = double.Parse(Console.ReadLine());
            Console.WriteLine("What is the new product's category id?");
            var categoryResponse = int.Parse(Console.ReadLine());
            prodRepo.CreateProduct(nameResponse, priceResponse, categoryResponse);
            //-----------updating a product-------------------------
            Console.WriteLine("What product would you like to update?");
            var oldProd = Console.ReadLine();
            Console.WriteLine("What is the product's new name?");
            var newProd = Console.ReadLine();
            Console.WriteLine("What is the product's new price?");
            var newPrice = double.Parse(Console.ReadLine());
            prodRepo.UpdateProduct(oldProd, newProd, newPrice);
            //-------------deleting a product-------------------------
            Console.WriteLine("Which product would you like to delete? (Product ID)");
            var delProd = int.Parse(Console.ReadLine());
            prodRepo.DeleteProduct(delProd);
            //-------------returning list of all products-------------
            var products = prodRepo.GetAllProducts();
            Console.WriteLine("ID --- Name --- Price --- Category ID");
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price} {prod.CategoryID}");
            }
            //-------------creating a new department-------------------
            var repo = new DapperDepartmentRepository(conn);
            Console.WriteLine("What is the new department's name?");
            var response = Console.ReadLine();
            repo.InsertNewDepartment(response);
            //--------------returning list of all departments--------------
            var departments = repo.GetAllDepartments();
            Console.WriteLine("ID --- Name");
            foreach(var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentID} {dept.Name}");
            }
        }

        static IEnumerable GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("ConnectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Departments;";

            using (conn)
            {
                conn.Open();
                List<string> allDepartments = new List<string>();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() == true)
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }

                return allDepartments;
            }
        }
    }
}
