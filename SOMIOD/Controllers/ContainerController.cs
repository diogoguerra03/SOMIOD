using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ContainerController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [HttpDelete]
        [Route("{application}/{container}")]
        public IHttpActionResult DeleteContainer(string application, string container)
        {
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
                        command = new SqlCommand("DELETE FROM Container WHERE Application_id = @appId AND name = @name", connection);
                        command.Parameters.AddWithValue("@name", container);
                        command.Parameters.AddWithValue("@appId", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return BadRequest("Nao existe aplicação com esse nome");
                    }


                }
                catch (SqlException ex)
                {
                    return NotFound();
                }
            }
        }



        [Route("{application}/{container}")]
        [HttpPost]
        public IHttpActionResult CreateSubOrDataOnContainer(string application, string container)
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
                int appId = 0;
                int containerId = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                if (res_type == "subscription")
                {
                    XmlNode subscriptionName = doc.SelectSingleNode("//subscription/name");
                    string name = subscriptionName.InnerText;
                    XmlNode subscriptionEndpoint = doc.SelectSingleNode("//subscription/endpoint");
                    string endpoint = subscriptionEndpoint.InnerText;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO Subscription (name, creation_dt, container_id, event, endpoint) VALUES (@name, @date, @conId, @event, @endpoint)", connection);
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
                            return BadRequest("Nome deve ser unico");
                        }
                    }
                }
                else if (res_type == "data")
                {
                    MqttClient mcClient = null;
                    string[] mStrTopicsInfo = { container };
                    string endpoint = null;
                    XmlNode dataName = doc.SelectSingleNode("//data/name");
                    string name = dataName.InnerText;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        int rowCount = 0;
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT endpoint FROM Subscription WHERE container_id = @conId", connection);
                        command.Parameters.AddWithValue("@conId", containerId);
                        SqlDataReader reader = command.ExecuteReader();
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
                            else if (endpoint.Substring(0, 4) == "http")
                            {
                                //Fazer pedido HTTP
                            }
                            rowCount++;
                        }
                        reader.Close();
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
                return BadRequest("Nome tem de ser unico");
            }
        }

        [HttpGet]
        [Route("{application}/{container}")]
        public IEnumerable<Object> GetContainer(string application, string container)
        {

            Container containerObject = null;
            int appId = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Application WHERE name = @name", connection);
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
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Container WHERE name = @name AND application_id = @id", connection);
                command.Parameters.AddWithValue("@name", container);
                command.Parameters.AddWithValue("@id", appId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    containerObject = new Container();
                    containerObject.Id = reader.GetInt32(0);
                    containerObject.Name = reader.GetString(1);
                    containerObject.creation_dt = reader.GetDateTime(2);
                    containerObject.ApplicationId = reader.GetInt32(3);
                }
                reader.Close();
            }

            var headers = HttpContext.Current.Request.Headers;
            string somiodDiscoverHeaderValue = headers.Get("somiod-discover");

            if (somiodDiscoverHeaderValue == "data")
            {
                List<Data> datas = new List<Data>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM data WHERE container_id = @id", connection);
                    command.Parameters.AddWithValue("@id", containerObject.Id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Data data = new Data();
                        data.Id = reader.GetInt32(0);
                        data.Name = reader.GetString(1);
                        data.Content = reader.GetString(2);
                        data.creation_dt = reader.GetDateTime(3);
                        data.ContainerId = reader.GetInt32(4);
                        datas.Add(data);
                    }
                }
                return datas;
            }
            if (somiodDiscoverHeaderValue == "sub")
            {
                List<Subscription> subs = new List<Subscription>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM subscription WHERE container_id = @id", connection);
                    command.Parameters.AddWithValue("@id", containerObject.Id);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Subscription subscription = new Subscription();
                        subscription.Id = reader.GetInt32(0);
                        subscription.Name = reader.GetString(1);
                        subscription.creation_dt = reader.GetDateTime(2);
                        subscription.ContainerId = reader.GetInt32(3);
                        subscription.Event = reader.GetString(4);
                        subscription.Endpoint = reader.GetString(5);

                        subs.Add(subscription);
                    }
                }
                return subs;
            }

            if (container != null)
            {
                List<Container> containers = new List<Container>();
                containers.Add(containerObject);
                return containers;
            }
            return null;

        }

        [HttpPut]
        [Route("{application}/{container}")]
        public IHttpActionResult PutContainer(string application, string container)
        {
            // update do container que se ecnontra dentro da aplicaçao
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
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            try
            {
                XmlNode request = doc.SelectSingleNode("/request");
                string res_type = request.Attributes["res_type"].InnerText;
                int appId = 0;
                int containerId = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
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

                    if (rowCount <= 0)
                    {
                        return NotFound();
                    }

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

                }



            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during XML processing
                return BadRequest("Nome tem de ser unico");
            }
        }
    }
}
