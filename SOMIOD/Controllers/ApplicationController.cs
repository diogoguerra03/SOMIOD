using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    public class ApplicationController : ApiController
    {
        string connectionString = SOMIOD.Properties.Settings.Default.ConnStr;

        [Route("api/applications")]
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

        [Route("api/applications/{id:int}")]
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

    }
}
