using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ApplicationController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllApplications()
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
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("response");
            doc.AppendChild(root);
            foreach (Application app in apps)
            {
                XmlElement application = doc.CreateElement("application");
                XmlElement name = doc.CreateElement("name");
                name.InnerText = app.Name;
                application.AppendChild(name);
                root.AppendChild(application);
            }

            string xmlContent = doc.OuterXml;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xmlContent, Encoding.UTF8, "application/xml"   );
            return response;
        }



        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateApplication()
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
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "No XML data provided");
                return response;
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
                                response = Request.CreateResponse(HttpStatusCode.OK, "Criado com sucesso");
                                return response;
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro ao criar applicaçao");
                                return response;
                            }
                        }
                        catch (SqlException ex)
                        {
                            // Handle the unique constraint violation
                            if (ex.Number == 2601 || ex.Number == 2627)
                            {
                                response = Request.CreateResponse(HttpStatusCode.Conflict, "Nome de aplicação já existe");
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
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Erro no res_type");
                    return response;
                }
            }
            catch (Exception ex)
            {

                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
            }
        }

        [Route("{application}")]
        [HttpDelete]
        public HttpResponseMessage DeleteApplication(string application)
        {
            HttpResponseMessage response;
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
                        response = Request.CreateResponse(HttpStatusCode.OK, "Eliminado com sucesso");
                        return response;
                    }
                    response = Request.CreateResponse(HttpStatusCode.NotFound, "Nao foi encontrada nenhuma aplicaçao com o nome "+ application);
                    return response;

                }
                catch (Exception)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro a eliminar da BD");
                    return response;
                }
            }
        }

        [HttpGet]
        [Route("{application}")]
        public HttpResponseMessage GetApplication(string application)
        {
            HttpResponseMessage response;
            Application applicationObject = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Application WHERE name = @name", connection);
                command.Parameters.AddWithValue("@name", application);
                SqlDataReader reader = command.ExecuteReader();
                int rowCount = 0;
                while (reader.Read())
                {
                    applicationObject = new Application();
                    applicationObject.Id = reader.GetInt32(0);
                    applicationObject.Name = reader.GetString(1);
                    applicationObject.creation_dt = reader.GetDateTime(2);
                    rowCount++;
                }
                reader.Close();
                if(rowCount == 0)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe nenhuma aplicação com o nome "+ application);
                    return response;
                }
            }
            

            List<Container> containers= new List<Container>();
            var headers = HttpContext.Current.Request.Headers;
            string somiodDiscoverHeaderValue = headers.Get("somiod-discover");

            if (somiodDiscoverHeaderValue == "container")
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
                XmlDocument docContainers = new XmlDocument();
                XmlDeclaration decContainers = docContainers.CreateXmlDeclaration("1.0", null, null);
                docContainers.AppendChild(decContainers);
                XmlElement rootCointainers = docContainers.CreateElement("response");
                docContainers.AppendChild(rootCointainers);
                foreach (Container container in containers)
                {
                    XmlElement containerElement = docContainers.CreateElement("container");
                    XmlElement name = docContainers.CreateElement("name");
                    name.InnerText = container.Name;
                    containerElement.AppendChild(name);
                    rootCointainers.AppendChild(containerElement);
                }
                string xmlContentContainer = docContainers.OuterXml;
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(xmlContentContainer, Encoding.UTF8, "application/xml");
                return response;
            }
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("response");
            doc.AppendChild(root);
            XmlElement appElement = doc.CreateElement("application");
            XmlElement idElement = doc.CreateElement("id");
            idElement.InnerText = applicationObject.Id.ToString();
            appElement.AppendChild(idElement);
            XmlElement nameElement = doc.CreateElement("name");
            nameElement.InnerText = applicationObject.Name;
            appElement.AppendChild(nameElement); ;
            XmlElement creationElement = doc.CreateElement("creation_dt");
            creationElement.InnerText = applicationObject.creation_dt.ToString();
            appElement.AppendChild(creationElement);
            root.AppendChild(appElement);

            string xmlContent = doc.OuterXml;
            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");
            return response;
        }

        [HttpPost]
        [Route("{application}")]
        public HttpResponseMessage CreateContainerOnApplication(string application)
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
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Erro a criar o XML");
                return response;
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
                                    response = Request.CreateResponse(HttpStatusCode.OK);
                                    return response;
                                }
                                else
                                {
                                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Nao foi possivel inserir na BD");
                                    return response;
                                }
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe aplicação com esse nome");
                                return response;
                            }


                        }
                        catch (SqlException ex)
                        {
                            // Handle the unique constraint violation
                            if (ex.Number == 2601 || ex.Number == 2627)
                            {
                                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nome do container tem de ser unico");
                                return response;
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro a inserir na BD");
                                return response;
                            }
                        }
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

        // Update application
        [HttpPut]
        [Route("{application}")]
        public HttpResponseMessage UpdateApplication(string application)
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

            if (docBytes == null || docBytes.Length == 0) { 
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

                if (res_type != "application")
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "res_type incorreto");
                    return response;

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
                            response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nao existe aplicaçao com esse nome");
                            return response;
                        }

                        command = new SqlCommand("UPDATE Application SET name = @name WHERE id = @id", connection);
                        // FALTA O DATE
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro a atualizar os dados da aplicaçao");
                            return response;
                        }

                        Console.WriteLine("UPDATE SUCCESSFULL!!!!");
                        response = Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                    catch (SqlException ex)
                    {
                        // Handle the unique constraint violation
                        if (ex.Number == 2601 || ex.Number == 2627)
                        {
                            response = Request.CreateResponse(HttpStatusCode.BadRequest, "Nome da aplicação ja existe");
                            return response;
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro a atualizar os dados da aplicaçao");
                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                return response;
            }

        }
    }
}
