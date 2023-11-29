using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private string connectionString = "server=localhost; user id=root; password=sean.net12345; database=testing; Port=3307;";
        // Store the database connection details in a string.
        public ActionResult Index()
        {
            List<Dictionary<string, string>> subjectList = new List<Dictionary<string, string>>();
            // Create an empty list to hold student information as dictionaries.

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open(); // open a connection to the database.

                string query = "SELECT subject_code,subject_name,unit,day,time FROM testtable ";
                // Define a query to get all data from a specific table.

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // Create a command, execute it, and retrieve data.

                    while (reader.Read())
                    {
                        // Loop through the retrieved data and store it as a dictionary.

                        var subject = new Dictionary<string, string>
                        {
                            ["SubjectCode"] = reader.GetString("subject_code"),
                            ["SubjectName"] = reader.GetString("subject_name"),
                            ["Unit"] = reader.GetString("unit"),
                            ["Day"] = reader.GetString("day"),
                            ["Time"] = reader.GetString("time")

                        };
                        subjectList.Add(subject);
                        // Extract student details and add them as a dictionary to the list.
                    }
                }
            }

            return View(subjectList);
            // Pass the list of dictionaries to the view for display.
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Layout(int data)
        {
            ViewBag.data = data;
            return View();
        }


        [HttpPost]
            public ActionResult Insert(string newSubjectCode, string newSubjectName, string newUnit, string newDay, string newTime)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                connection.Open();


                string query = "INSERT INTO testtable (subject_code, subject_name,unit, day, time) VALUES (@SubjectCode, @SubjectName, @Unit, @Day, @Time)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SubjectCode", newSubjectCode);
                    cmd.Parameters.AddWithValue("@SubjectName", newSubjectName);
                    cmd.Parameters.AddWithValue("@Unit", newUnit);
                    cmd.Parameters.AddWithValue("@Day", newDay);
                    cmd.Parameters.AddWithValue("@Time", newTime);

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult Update(string newSubjectCode, string newSubjectName, string newUnit, string newDay, string newTime)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE testtable SET subject_name = @SubjectName,unit = @Unit,day = @Day,time = @Time WHERE subject_code = @SubjectCode";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SubjectCode", newSubjectCode);
                    cmd.Parameters.AddWithValue("@SubjectName", newSubjectName);
                    cmd.Parameters.AddWithValue("@Unit", newUnit);
                    cmd.Parameters.AddWithValue("@Day", newDay);
                    cmd.Parameters.AddWithValue("@Time", newTime);

                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

    }


}