using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace APP_MANAGER
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:59454/api/somiod";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadListBoxApps();
        }

        private void loadListBoxApps()
        {
            listBoxApps.Items.Clear();

            // request to the server for the list of applications
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI);
            request.Method = "GET";
            request.ContentType = "application/xml";
            request.Headers.Add("somiod-discover", "application");

            try
            {
                // Get the response from the server
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Check if the request was successful
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.StatusDescription}");
                    }
                    else
                    {
                        byte[] docBytes;
                        // Get the response stream
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                responseStream.CopyTo(memoryStream);
                                docBytes = memoryStream.ToArray();
                            }
                        }

                        if (docBytes == null || docBytes.Length == 0)
                        {
                            MessageBox.Show("Erro ao ler xml");
                            return;
                        }
                        string xmlContent = Encoding.UTF8.GetString(docBytes);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlContent);
                        XmlNodeList applications = doc.SelectNodes("//application");
                        foreach (XmlNode application in applications)
                        {
                            listBoxApps.Items.Add(application["name"].InnerText);
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }

        }

        private void btnDeleteApp_Click(object sender, EventArgs e)
        {
            if (listBoxApps.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma aplicação");
                return;
            }

            String app = listBoxApps.SelectedItem.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + app);

            request.Method = "DELETE";
            request.ContentType = "application/xml";

            try
            {
                // Get the response from the server
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Check if the request was successful
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.StatusDescription}");
                    }
                    else
                    {
                        MessageBox.Show("Eliminado com sucesso");
                        loadListBoxApps();
                    }
                }
            }
            catch (WebException ex)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    XmlDocument docResponse = new XmlDocument();
                    docResponse.LoadXml(responseContent);

                    MessageBox.Show(docResponse.InnerText);
                }
            }
        }

        private void buttonUpdateAppName_Click(object sender, EventArgs e)
        {
            if (listBoxApps.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma aplicação");
                return;
            }

            String app = listBoxApps.SelectedItem.ToString();
            String nameUpdated = txtNameToUpdate.Text;

            if (nameUpdated == null || nameUpdated.Length == 0)
            {
                MessageBox.Show("Insira um nome para a aplicação");
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "application");
            doc.AppendChild(root);
            XmlElement application = doc.CreateElement("application");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = nameUpdated;
            application.AppendChild(name);
            root.AppendChild(application);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + app);

            request.Method = "PUT";
            request.ContentType = "application/xml";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
            request.ContentLength = xmlBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(xmlBytes, 0, xmlBytes.Length);
            }

            try
            {
                // Get the response from the server 
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Check if the request was successful
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.StatusDescription}");
                    }
                    loadListBoxApps();
                    MessageBox.Show("Nome atualizado com sucesso com sucesso");
                }
            }
            catch (WebException ex)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    XmlDocument docResponse = new XmlDocument();
                    docResponse.LoadXml(responseContent);

                    MessageBox.Show(docResponse.InnerText);
                }
            }
        }

        private void btnCreateApp_Click(object sender, EventArgs e)
        {
            String app = txtNameToCreate.Text;
            if (app == null || app.Length == 0)
            {
                MessageBox.Show("Insira um nome para a aplicação");
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "application");
            doc.AppendChild(root);
            XmlElement application = doc.CreateElement("application");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = app;
            application.AppendChild(name);
            root.AppendChild(application);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI);

            request.Method = "POST";
            request.ContentType = "application/xml";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
            request.ContentLength = xmlBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(xmlBytes, 0, xmlBytes.Length);
            }
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Check if the request was successful
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show($"Error: {response.StatusCode} - {response.StatusDescription}");
                    }
                    else
                    {
                        loadListBoxApps();
                        MessageBox.Show("Criado com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {

                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    XmlDocument docResponse = new XmlDocument();
                    docResponse.LoadXml(responseContent);

                    MessageBox.Show(docResponse.InnerText);

                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadListBoxApps();
        }
    }
}
