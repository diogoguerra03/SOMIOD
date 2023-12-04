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
using uPLibrary.Networking.M2Mqtt;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SubscriptionAndDataController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [Route("{application}/{container}")]
        [HttpPost]
        public IHttpActionResult CreateSubOrData(string application, string container)
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

                if (res_type == "subscription")
                {
                    XmlNode subscriptionName = doc.SelectSingleNode("//subscription/name");
                    string name = subscriptionName.InnerText;
                    XmlNode subscriptionEndpoint = doc.SelectSingleNode("//subscription/endpoint");
                    string endpoint = subscriptionEndpoint.InnerText;

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
                                command = new SqlCommand("SELECT id FROM Container WHERE name = @name", connection);
                                command.Parameters.AddWithValue("@name", container);
                                reader = command.ExecuteReader();
                                rowCount = 0;
                                while (reader.Read())
                                {
                                    containerId = reader.GetInt32(0);
                                    rowCount++;
                                }
                                reader.Close();
                                if (rowCount > 0) { 

                                    command = new SqlCommand("INSERT INTO Subscription (name, creation_dt, container_id, event, endpoint) VALUES (@name, @date, @conId, @event, @endpoint)", connection);
                                    command.Parameters.AddWithValue("@date", DateTime.Now);
                                    command.Parameters.AddWithValue("@name", name);
                                    command.Parameters.AddWithValue("@conId", containerId);
                                    command.Parameters.AddWithValue("@event", "1");
                                    command.Parameters.AddWithValue("@endpoint", endpoint);

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
                else if(res_type == "data")
                {
                    MqttClient mcClient = null;
                    string[] mStrTopicsInfo = { container };
                    string endpoint = null;
                    XmlNode dataName = doc.SelectSingleNode("//data/name");
                    string name = dataName.InnerText;
                    using (SqlConnection connection = new SqlConnection(connectionString)) { 
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT id FROM Container WHERE name = @name", connection);
                        command.Parameters.AddWithValue("@name", container);
                        SqlDataReader reader = command.ExecuteReader();
                        int rowCount = 0;
                        int containerId = 0;
                        while (reader.Read())
                        {
                            containerId = reader.GetInt32(0);
                            rowCount++;
                        }
                        reader.Close();
                        command = new SqlCommand("SELECT endpoint FROM Subscription WHERE container_id = @conId", connection);
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
                                mcClient.Publish(container, Encoding.UTF8.GetBytes(name));
                            }
                            else if (endpoint.Substring(0, 4) == "htttp")
                            {
                                //Fazer pedido HTTP
                            }
                            rowCount++;
                        }
                        if (rowCount > 0)
                        {
                            command = new SqlCommand("INSERT INTO Data (name, content, creation_dt, container_id) VALUES (@name,@content, @date, @conId)", connection);
                            command.Parameters.AddWithValue("@date", DateTime.Now);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@content", "Teste"); //Mudar depois
                            command.Parameters.AddWithValue("@conId", containerId);
                            command.ExecuteNonQuery();
                            return Ok();
                        }
                        return BadRequest();
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
    }
}
