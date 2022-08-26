using Microsoft.AspNetCore.Mvc;
using MyProject2.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MyProject2.Controllers
{
    public class HomeController : Controller
    {
        public string conString = "Data Source = LTP_RD_411; Initial Catalog = test; Integrated Security = True;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data()
        {
            List<User2> users = new List<User2>();
            string select = "select * from Users2";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User2((int)reader["PersonID"], reader["LastName"].ToString(), reader["FirstName"].ToString(), reader["Address"].ToString(), reader["City"].ToString()));

            }
            cmd.Dispose();
            conn.Close();
            return View(users);
        }

        [HttpPost]
        public IActionResult Insert(User2 u)
        {
            //var user = JsonConvert.DeserializeObject<User>(requestData);
            //Console.WriteLine(user.Username);

            Console.WriteLine(u);
            DateTime localDate = DateTime.Now;
            string Insert = "Insert into Users2(lastname, firstname, address, city) values(@lastname, @firstname, @address, @city)";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Insert, conn);
            cmd.Parameters.AddWithValue("@lastname", u.LastName);
            cmd.Parameters.AddWithValue("@firstname", u.FirstName);
            cmd.Parameters.AddWithValue("@address", u.Address);
            cmd.Parameters.AddWithValue("@city", u.City);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return Redirect("/Home/Data");

        }

        public RedirectResult DeleteProcess(int id)
        {

            string Delete = "delete from Users2 where PersonID = @id";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Delete, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            return Redirect("/Home/Data");

        }

        public IActionResult EditProcess(int id)
        {
            Console.WriteLine(id);
            User2 u = null;
            string update = "select * from Users2 where PersonID=@id";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                u = new User2((int)reader["PersonID"], reader["LastName"].ToString(), reader["FirstName"].ToString(), reader["Address"].ToString(), reader["City"].ToString());
            }
            conn.Close();

            return View(u);
        }

        public RedirectResult Update(User2 u)
        {

            string update = "update Users2 set lastname=@lastname, firstname=@firstname, address=@address, city=@city where personid = @id";
            SqlConnection conn = new SqlConnection(conString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@lastname", u.LastName);
            cmd.Parameters.AddWithValue("@firstname", u.FirstName);
            cmd.Parameters.AddWithValue("@address", u.Address);
            cmd.Parameters.AddWithValue("@city", u.City);
            cmd.Parameters.AddWithValue("@id", u.PersonID);
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine("Row affected " + u.LastName);
            conn.Close();
            return Redirect("/Home/Data");
        }


    }
}