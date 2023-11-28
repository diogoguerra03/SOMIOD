using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ApplicationController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [Route("applications")]
        public IEnumerable<Application> GetAllApplications() 
        {
            List<Application> apps = new List<Application>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Application", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Application app = new Application();
                    app.Id = reader.GetInt32(0);
                    app.Name = reader.GetString(1);
                    app.creation_dt = reader.GetDateTime(2);
                    apps.Add(app);
                }
            }

            return apps;
        }

        [Route("applications/{id:int}")]
        public IHttpActionResult GetApplication(int id)
        {
            Application application = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Application WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    application = new Application();
                    application.Id = reader.GetInt32(0);
                    application.Name = reader.GetString(1);
                    application.creation_dt = reader.GetDateTime(2);
                }
            }

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateApplication()
        {

            byte[] docBytes;
            using (Stream stream = Request.Content.ReadAsStreamAsync().Result)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    docBytes = memoryStream.ToArray();
                }
            }

            if (docBytes == null || docBytes.Length == 0)
            {
                return BadRequest("No data provided");
            }

            // Convert the bytes to a string for further processing (if needed)
            string xmlContent = Encoding.UTF8.GetString(docBytes);


            if (xmlContent == null)
            {
                // Handle the case where no XML data is provided
                return BadRequest("No XML data provided");
            }


            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xmlContent);

            try
            {
                XmlNode request = doc.SelectSingleNode("/request");
                string res_type = request.Attributes["res_type"].InnerText;

                if (res_type == "application")
                {
                    XmlNode application = doc.SelectSingleNode("//application/name");
                    string name = application.InnerText;


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("INSERT INTO Application (name, creation_dt) VALUES (@name, @date)", connection);
                            command.Parameters.AddWithValue("@date", DateTime.Now);
                            command.Parameters.AddWithValue("@name", name);

                            // Execute the command
                            int rowsAffected = command.ExecuteNonQuery();

                            // Check if the insertion was successful
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Insertion successful!");
                            }
                            else
                            {
                                Console.WriteLine("Insertion failed!");
                            }
                        }
                        catch (SqlException ex)
                        {
                            // Handle the unique constraint violation
                            if (ex.Number == 2601 || ex.Number == 2627)
                            {
                                Console.WriteLine("Name already exists. Choose a unique name.");
                            }
                            else
                            {
                                Console.WriteLine("Insertion failed: " + ex.Message);
                            }
                        }
                    }

                }
                else
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during XML processing
                return InternalServerError(ex);
            }
        }

    }
}
