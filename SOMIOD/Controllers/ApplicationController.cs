using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ApplicationController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [HttpGet]
        [Route("")]
        public IEnumerable<Application> GetAllApplications()
        {
            List<Application> apps = new List<Application>();
            var headers = HttpContext.Current.Request.Headers;
            string somiodDiscoverHeaderValue = headers.Get("somiod-discover");

            if (somiodDiscoverHeaderValue == "application")
            {
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
            }
            return apps;
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

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Insertion successful!");
                                return Ok();
                            }
                            else
                            {
                                Console.WriteLine("Insertion failed!");
                                return InternalServerError();
                            }
                        }
                        catch (SqlException ex)
                        {
                            // Handle the unique constraint violation
                            if (ex.Number == 2601 || ex.Number == 2627)
                            {
                                Console.WriteLine("Name already exists. Choose a unique name.");
                                return BadRequest("Nome deve ser unico");
                            }
                            else
                            {
                                Console.WriteLine("Insertion failed: " + ex.Message);
                                return BadRequest("Erro no insert da DB");
                            }
                        }
                    }

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during XML processing
                return InternalServerError(ex);
            }
        }

        [Route("{application}")]
        [HttpDelete]
        public IHttpActionResult DeleteApplication(string application)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Application WHERE name = @name", connection);
                    command.Parameters.AddWithValue("@name", application);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    return NotFound();

                }
                catch (Exception)
                {

                    return NotFound();
                }
            }
        }

        [HttpGet]
        [Route("{application}")]
        public IEnumerable<Object> GetApplication(string application)
        {

            Application applicationObject = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Application WHERE name = @name", connection);
                command.Parameters.AddWithValue("@name", application);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    applicationObject = new Application();
                    applicationObject.Id = reader.GetInt32(0);
                    applicationObject.Name = reader.GetString(1);
                    applicationObject.creation_dt = reader.GetDateTime(2);
                }
                reader.Close();
            }

            if (application == null)
            {
                return null;
            }

            List<Container> containers = new List<Container>();
            var headers = HttpContext.Current.Request.Headers;
            string somiodDiscoverHeaderValue = headers.Get("somiod-discover");

            if (somiodDiscoverHeaderValue == "application")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM container WHERE application_id = @id", connection);
                    command.Parameters.AddWithValue("@id", applicationObject.Id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Container container = new Container();
                        container.Id = reader.GetInt32(0);
                        container.Name = reader.GetString(1);
                        container.creation_dt = reader.GetDateTime(2);
                        container.ApplicationId = reader.GetInt32(3);
                        containers.Add(container);
                    }
                }
                return containers;
            }
            List<Application> applications = new List<Application>();
            applications.Add(applicationObject);
            return applications;
        }

        [HttpPost]
        [Route("{application}")]
        public IHttpActionResult CreateContainerOnApplication(string application)
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

                if (res_type == "container")
                {
                    XmlNode containerName = doc.SelectSingleNode("//container/name");
                    string name = containerName.InnerText;
                    int id = 0;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("SELECT id FROM Application WHERE name = @name", connection);
                            command.Parameters.AddWithValue("@name", application);
                            SqlDataReader reader = command.ExecuteReader();
                            int rowCount = 0;
                            while (reader.Read())
                            {
                                id = reader.GetInt32(0);
                                rowCount++;
                            }
                            reader.Close();
                            if (rowCount > 0)
                            {
                                command = new SqlCommand("INSERT INTO Container (name, creation_dt, application_id) VALUES (@name, @date, @appId)", connection);
                                command.Parameters.AddWithValue("@date", DateTime.Now);
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@appId", id);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Insertion successful!");
                                    return Ok();
                                }
                                else
                                {
                                    Console.WriteLine("Insertion failed!");
                                    return InternalServerError();
                                }
                            }
                            else
                            {
                                return BadRequest("Nao existe aplicação com esse nome");
                            }


                        }
                        catch (SqlException ex)
                        {
                            // Handle the unique constraint violation
                            if (ex.Number == 2601 || ex.Number == 2627)
                            {
                                Console.WriteLine("Name already exists. Choose a unique name.");
                                return BadRequest("Nome deve ser unico");
                            }
                            else
                            {
                                Console.WriteLine("Insertion failed: " + ex.Message);
                                return BadRequest("Erro no insert da DB");
                            }
                        }
                    }

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during XML processing
                return InternalServerError(ex);
            }
        }

        // Update application
        [HttpPut]
        [Route("{application}")]
        public IHttpActionResult UpdateApplication(string application)
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
                return BadRequest("No data provided");

            string xmlContent = Encoding.UTF8.GetString(docBytes);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            try
            {
                XmlNode request = doc.SelectSingleNode("/request");
                string res_type = request.Attributes["res_type"].InnerText;

                if (res_type != "application")
                {
                    return BadRequest("Response type should be application");

                }

                XmlNode applicationName = doc.SelectSingleNode("//application/name");
                string name = applicationName.InnerText;
                int id = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT id FROM Application WHERE name = @name", connection);
                        command.Parameters.AddWithValue("@name", application);
                        SqlDataReader reader = command.ExecuteReader();
                        int rowCount = 0;
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                            rowCount++;
                        }
                        reader.Close();
                        if (rowCount <= 0)
                        {
                            return BadRequest("Nao existe aplicação com esse nome");
                        }

                        command = new SqlCommand("UPDATE Application SET name = @name WHERE id = @id", connection);
                        // FALTA O DATE
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            return BadRequest("Nao foi possivel efetuar o update");
                        }

                        Console.WriteLine("UPDATE SUCCESSFULL!!!!");
                        return Ok();
                    }
                    catch (SqlException ex)
                    {
                        // Handle the unique constraint violation
                        if (ex.Number == 2601 || ex.Number == 2627)
                        {
                            Console.WriteLine("Name already exists. Choose a unique name.");
                            return BadRequest("Nome deve ser unico");
                        }
                        else
                        {
                            Console.WriteLine("Insertion failed: " + ex.Message);
                            return BadRequest("Erro no insert da DB");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during XML processing
                return InternalServerError(ex);
            }

        }
    }
}
