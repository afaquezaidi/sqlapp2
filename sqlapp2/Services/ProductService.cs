using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using sqlapp.Models;
using Microsoft.Extensions.Configuration;

namespace sqlapp2.Services
{
    public class ProductService : IProductService
    {
        //private static string db_source = "demoappdbserver1000.database.windows.net";
        //private static string db_user = "sqladmin";
        //private static string db_password = "Milkyway@123";
        //private static string db_database = "demoappdb";
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _product_list = new List<Product>();
            string statement = "select ProductID,ProductName,Quantity from Products";

            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _product_list.Add(product);
                }
            }
            conn.Close();
            return _product_list;
        }
    }
}
