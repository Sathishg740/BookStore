using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }
        public AdminController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            List<Book> UserList = new List<Book>();
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "Select * From Book";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Book book = new Book();
                        book.Type = Convert.ToString(dataReader["Type"]);
                        book.Name = Convert.ToString(dataReader["Name"]);

                        book.Cost = Convert.ToInt32(dataReader["Cost"]);
                        book.Author = Convert.ToString(dataReader["Author"]);

                        UserList.Add(book);
                    }
                }
                connection.Close();

            }
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Book (Type,Name,Cost,Author) Values ( '{book.Type}','{book.Name}', '{book.Cost}', '{book.Author}' )";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Details()
        {
            List<Book> UserList = new List<Book>();
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "Select * From Book";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Book book= new Book();
                        book.Type = Convert.ToString(dataReader["Type"]);
                        book.Name = Convert.ToString(dataReader["Name"]);
                  
                        book.Cost = Convert.ToInt32(dataReader["Cost"]);
                        book.Author = Convert.ToString(dataReader["Author"]);
                      
                        UserList.Add(book);
                    }
                }
                connection.Close();

            }
            return View();
        }
    }
}
