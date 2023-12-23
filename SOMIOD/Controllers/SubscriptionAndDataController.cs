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
using System.Web.UI.WebControls;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SubscriptionAndDataController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [Route("{application}/{container}/sub/{name}")]
        [HttpDelete]
        public IHttpActionResult DeleteSubscription(string application, string container, string name)
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

            int appId = 0;
            int containerId = 0;
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
                        appId = reader.GetInt32(0);
                        rowCount++;
                    }
                    reader.Close();
                    if (rowCount > 0)
                    {
                        command = new SqlCommand("SELECT id FROM Container WHERE name = @name AND application_id = @appId", connection);
                        command.Parameters.AddWithValue("@appId", appId);
                        command.Parameters.AddWithValue("@name", container);
                        reader = command.ExecuteReader();
                        rowCount = 0;
                        while (reader.Read())
                        {
                            containerId = reader.GetInt32(0);
                            rowCount++;
                        }
                        reader.Close();
                        if (rowCount > 0)
                        {

                            command = new SqlCommand("DELETE FROM Subscription WHERE name = @name AND container_id = @conId", connection);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@conId", containerId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                return Ok();
                            }
                            else
                            {
                                return InternalServerError();
                            }
                        }
                        else
                        {
                            return BadRequest("Nao existe aplicação com esse nome");
                        }
                    }
                    else
                    {
                        return BadRequest("Nao existe container com esse nome");
                    }

                }
                catch (SqlException ex)
                {
                    // Handle the unique constraint violation
                    if (ex.Number == 2601 || ex.Number == 2627)
                    {
                        return BadRequest("Nome deve ser unico");
                    }
                    else
                    {
                        return BadRequest("Erro no insert da DB");
                    }
                }

            }
        }

        [Route("{application}/{container}/data/{name}")]
        [HttpDelete]
        public IHttpActionResult DeleteData(string application, string container, string name)
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

            int appId = 0;
            int containerId = 0;
            MqttClient mcClient = null;
            string[] mStrTopicsInfo = { container };
            string endpoint = null;
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
                        appId = reader.GetInt32(0);
                        rowCount++;
                    }
                    reader.Close();
                    if (rowCount > 0)
                    {
                        command = new SqlCommand("SELECT id FROM Container WHERE name = @name AND application_id = @appId", connection);
                        command.Parameters.AddWithValue("@appId", appId);
                        command.Parameters.AddWithValue("@name", container);
                        reader = command.ExecuteReader();
                        rowCount = 0;
                        while (reader.Read())
                        {
                            containerId = reader.GetInt32(0);
                            rowCount++;
                        }
                        reader.Close();
                        if (rowCount > 0)
                        {
                            rowCount = 0;
                            command = new SqlCommand("SELECT endpoint FROM Subscription WHERE container_id = @conId AND event = 2", connection);
                            command.Parameters.AddWithValue("@conId", containerId);
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                endpoint = reader.GetString(0);
                                if (endpoint.Substring(0, 4) == "mqtt")
                                {
                                    mcClient = new MqttClient(IPAddress.Parse(endpoint.Substring(7)));
                                    mcClient.Connect(Guid.NewGuid().ToString());
                                    if (!mcClient.IsConnected)
                                    {
                                        Console.WriteLine("Error connecting to message broker...");
                                    }
                                    mcClient.Publish(container, Encoding.UTF8.GetBytes("Data eliminada...")); //TODO Mudar esta mensagem
                                }
                                else if (endpoint.Substring(0, 4) == "http")
                                {
                                    //Fazer pedido HTTP
                                }
                                rowCount++;
                            }
                            reader.Close();
                            command = new SqlCommand("DELETE FROM Data WHERE name = @name AND container_id = @conId", connection);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@conId", containerId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                return Ok();
                            }
                            else
                            {
                                return InternalServerError();
                            }
                        }
                        else
                        {
                            return BadRequest("Nao existe aplicação com esse nome");
                        }
                    }
                    else
                    {
                        return BadRequest("Nao existe container com esse nome");
                    }

                }
                catch (SqlException ex)
                {
                    // Handle the unique constraint violation
                    if (ex.Number == 2601 || ex.Number == 2627)
                    {
                        return BadRequest("Nome deve ser unico");
                    }
                    else
                    {
                        return BadRequest("Erro no insert da DB");
                    }
                }

            }
        }

        [HttpGet]
        [Route("{application}/{container}/data/{data}")]
        public IHttpActionResult GetData(string application, string container, string data)
        {

            int appId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id FROM Application WHERE name = @name", connection);
                command.Parameters.AddWithValue("@name", application);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    appId = reader.GetInt32(0);
                }
                reader.Close();
            }

            if (appId == -1)
            {
                return NotFound();
            }
            int containerId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id FROM Container WHERE name = @name AND application_id = @id", connection);
                command.Parameters.AddWithValue("@name", container);
                command.Parameters.AddWithValue("@id", appId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    containerId = reader.GetInt32(0);
                }
                reader.Close();
            }
            if(containerId == -1)
            {
                return NotFound();
            }
            Data dataObject = new Data();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM data WHERE name = @name AND container_id= @id", connection);
                command.Parameters.AddWithValue("@name", data);
                command.Parameters.AddWithValue("@id", containerId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dataObject.Id = reader.GetInt32(0);
                    dataObject.Name = reader.GetString(1);
                    dataObject.Content = reader.GetString(2);
                    dataObject.creation_dt = reader.GetDateTime(3);
                    dataObject.ContainerId = reader.GetInt32(4);
                    count++;
                }
                reader.Close();
            }
            if(count == 0)
            {
                return NotFound();
            }
            return Ok(dataObject);
        }


        [HttpGet]
        [Route("{application}/{container}/sub/{subscription}")]
        public IHttpActionResult GetSubscription(string application, string container, string subscription)
        {

            int appId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id FROM Application WHERE name = @name", connection);
                command.Parameters.AddWithValue("@name", application);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    appId = reader.GetInt32(0);
                }
                reader.Close();
            }

            if (appId == -1)
            {
                return NotFound();
            }
            int containerId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT id FROM Container WHERE name = @name AND application_id = @id", connection);
                command.Parameters.AddWithValue("@name", container);
                command.Parameters.AddWithValue("@id", appId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    containerId = reader.GetInt32(0);
                }
                reader.Close();
            }
            if (containerId == -1)
            {
                return NotFound();
            }
            Subscription subObject = new Subscription();
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM subscription WHERE name = @name AND container_id= @id", connection);
                command.Parameters.AddWithValue("@name", subscription);
                command.Parameters.AddWithValue("@id", containerId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    subObject.Id = reader.GetInt32(0);
                    subObject.Name = reader.GetString(1);
                    subObject.creation_dt = reader.GetDateTime(2);
                    subObject.ContainerId = reader.GetInt32(3);
                    subObject.Event = reader.GetInt32(4);
                    subObject.Endpoint = reader.GetString(5);
                    count++;
                }
                reader.Close();
            }
            if (count == 0)
            {
                return NotFound();
            }
            return Ok(subObject);
        }
    }
}
