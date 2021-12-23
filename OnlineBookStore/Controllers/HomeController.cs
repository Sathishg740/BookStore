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
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(Register register)
        {

            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Register (Name, Email, Password) Values ('{register.Name}', '{register.Email}','{register.Password}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = 1;
            return View();  
        }
        public IActionResult Login(Register register)
        {
            var check = 1;
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select Email,Password From Register where Email = '{register.LoginEmail}' and Password = '{register.LoginPassword}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    var validate = command.ExecuteScalar();
                    if (validate != null)
                    {
                        check = 0;

                    }
                    connection.Close();
                }

            }
            if (check == 0)
            {

                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.Query = 1;
            }

            return RedirectToAction("Registration");

        }
        public IActionResult ActivityBooks()
        {
            return View();
        }
        public IActionResult ActivityDetails()
        {
            return View();
        }
        public IActionResult StoryBooks()
        {
            return View();
        }
        public IActionResult SubjectBooks()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult CustomerDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerDetails(CustomerDetails user)
        {

            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into CustomerDetails (Name,Email,Address,State,Pincode) Values ('{user.Name}', '{user.Email}','{user.Address}', '{user.State}','{user.Pincode}' )";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            List<CustomerDetails> UserList = new List<CustomerDetails>();
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "Select * From CustomerDetails ";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CustomerDetails user = new CustomerDetails();

                        user.Name = Convert.ToString(dataReader["Name"]);
                        user.Email = Convert.ToString(dataReader["Email"]);
                        user.Address = Convert.ToString(dataReader["Address"]);
                      
                        user.State = Convert.ToString(dataReader["State"]);
                        user.Pincode = Convert.ToInt32(dataReader["Pincode"]);
                        UserList.Add(user);
                    }
                }
                connection.Close();

            }
            return View(UserList);
        }
    }
}
