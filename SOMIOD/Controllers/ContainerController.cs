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
        public HttpResponseMessage DeleteContainer(string application, string container)
        {
            HttpResponseMessage response;
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
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe um container com o nome "+container);
                            return response;
                        }
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe aplicaçao com o nome" + application);
                        return response;
                    }

                }
                catch (SqlException ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro ao dar delete da BD " + ex);
                    return response;
                }
            }
        }



        [Route("{application}/{container}")]
        [HttpPost]
        public HttpResponseMessage CreateSubOrDataOnContainer(string application, string container)
        {
            HttpResponseMessage response;
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
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "No data provided");
                return response;
            }

            string xmlContent = Encoding.UTF8.GetString(docBytes);
            if (xmlContent == null)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Erro a converter data em XML");
                return response;
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
                        if(rowCount < 1)
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe um container com o nome " + container);
                            return response;
                        }
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe uma aplicaçao com o nome "+ application);
                        return response;
                    }
                }

                if (res_type == "subscription")
                {
                    XmlNode subscriptionName = doc.SelectSingleNode("//subscription/name");
                    string name = subscriptionName.InnerText;
                    XmlNode subscriptionEndpoint = doc.SelectSingleNode("//subscription/endpoint");
                    string endpoint = subscriptionEndpoint.InnerText;
                    XmlNode subscriptionEvent = doc.SelectSingleNode("//subscription/event");
                    string subEvent = subscriptionEvent.InnerText;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("INSERT INTO Subscription (name, creation_dt, container_id, event, endpoint) VALUES (@name, @date, @conId, @event, @endpoint)", connection);
                            command.Parameters.AddWithValue("@date", DateTime.Now);
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@conId", containerId);
                            command.Parameters.AddWithValue("@event", subEvent);
                            command.Parameters.AddWithValue("@endpoint", endpoint);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                response = Request.CreateResponse(HttpStatusCode.OK);
                                return response;
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.BadRequest, "O nome tem de ser unico");
                                return response;
                            }
                        }
                        catch (SqlException ex)
                        {

                            if (ex.Number == 2601 || ex.Number == 2627)
                            {
                                response = Request.CreateResponse(HttpStatusCode.Conflict, "Nome da subscricao já existe");
                                return response;
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro no insert da DB");
                                return response;
                            }
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
                    XmlNode dataContent = doc.SelectSingleNode("//data/content");
                    string content = dataContent.InnerText;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        int rowCount = 0;
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT endpoint FROM Subscription WHERE container_id = @conId AND event = 1", connection);
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
                                mcClient.Publish(container, Encoding.UTF8.GetBytes(content));
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
                            try
                            {
                                command = new SqlCommand("INSERT INTO Data (name, content, creation_dt, container_id) VALUES (@name,@content, @date, @conId)", connection);
                                command.Parameters.AddWithValue("@date", DateTime.Now);
                                command.Parameters.AddWithValue("@name", name);
                                command.Parameters.AddWithValue("@content", content); //Mudar depois
                                command.Parameters.AddWithValue("@conId", containerId);
                                command.ExecuteNonQuery();
                                response = Request.CreateResponse(HttpStatusCode.OK);
                                return response;
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 2601 || ex.Number == 2627)
                                {
                                    response = Request.CreateResponse(HttpStatusCode.Conflict, "Nome de data já existe");
                                    return response;
                                }
                                else
                                {
                                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro no insert da DB");
                                    return response;
                                }
                            } 
                        }
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existen subscriçoes para se enviar a data");
                        return response;
                    }

                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "res_type inválido");
                    return response;
                }
            }
            catch (Exception ex)
            {

                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
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
                List<string> datasNames = new List<string>();
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
                        datasNames.Add(data.Name);
                    }
                }
                return datasNames;
            }
            if (somiodDiscoverHeaderValue == "sub")
            {
                List<string> subsNames = new List<string>();
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

                        subsNames.Add(subscription.Name);
                    }
                }
                return subsNames;
            }

            if (container != null)
            {
                List<string> containersNames = new List<string>();
                containersNames.Add(containerObject.Name);
                return containersNames;
            }
            return null;

        }

        [HttpPut]
        [Route("{application}/{container}")]
        public HttpResponseMessage PutContainer(string application, string container)
        {
            HttpResponseMessage response;
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
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "No data provided");
                return response;
            }

            string xmlContent = Encoding.UTF8.GetString(docBytes);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            try
            {
                XmlNode request = doc.SelectSingleNode("/request");
                string res_type = request.Attributes["res_type"].InnerText;

                if (res_type != "container")
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "res_type inválido");
                    return response;

                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
            }


            try
            {
                XmlNode containerName = doc.SelectSingleNode("//container/name");
                string name = containerName.InnerText;

                int appId = 0;
                int containerId = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Descobrir o id da applicaçao que possui o container que queremos
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
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Aplicação com o nome "+ application +" nao existe");
                        return response;
                    }


                    // Descobrir o Id do container que de facto queremos atualizar que possua o id da aplicaçao
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

                    if (rowCount <= 0)
                    {
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Container com o nome "+ container +" nao existe");
                        return response;
                    }

                    // Atualizar o nome do container desejado
                    command = new SqlCommand("UPDATE Container SET name = @name WHERE id= @id", connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@id", containerId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected <= 0)
                    {
                        response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao foi possivel efetuar o update");
                        return response;
                    }

                    Console.WriteLine("UPDATE SUCCESSFULL!!!!");
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    return response;

                }
            }
            catch (SqlException ex)
            {
                // Handle the unique constraint violation
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "O nome do container ja existe");
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro a dar update na BD");
                    return response;
                }
            }
        }
    }
}
