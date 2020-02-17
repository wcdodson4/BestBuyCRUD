using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;
using System.Linq;

namespace BestBuyCRUD
{
    public class DapperDepartmentRepository
    {
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM Departments;").ToList();
        }

        public void InsertNewDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO Departments (Name) VALUES (@departmentName);", new { departmentName = newDepartmentName });
        }
    }
}
