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

namespace APP_CONTAINER_MANAGER
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:59454/api/somiod";
        string application;
        public Form1()
        {
            InitializeComponent();
        }

        private void getContainerButton_Click(object sender, EventArgs e)
        {
            if (appTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o nome da aplicação");
                return;
            }
            application = appTextBox.Text;
            loadContainerListBox();
        }

        private void createContainerButton_Click(object sender, EventArgs e)
        {
            if (application.Length == 0)
            {
               MessageBox.Show("Insira o nome da aplicação");
                return;
            }
            if(containerNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o nome do container");
                return;
            }
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "container");
            doc.AppendChild(root);
            XmlElement container = doc.CreateElement("container");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = containerNameTextBox.Text;
            container.AppendChild(name);
            root.AppendChild(container);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application);

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
                        MessageBox.Show("Criado com sucesso");
                        loadContainerListBox();
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
        
        public void loadContainerListBox()
        {
            containersListBox.Items.Clear();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application);
            request.Method = "GET";
            request.ContentType = "application/xml";
            request.Headers.Add("somiod-discover", "container");

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
                        XmlNodeList contianers = doc.SelectNodes("//container");
                        foreach (XmlNode container in contianers)
                        {
                            containersListBox.Items.Add(container["name"].InnerText);
                        }

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

        private void deleteContainerButton_Click(object sender, EventArgs e)
        {
            if(containersListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um container");
                return;
            }

            string container = containersListBox.SelectedItem.ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container);

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
                        loadContainerListBox();
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

        private void updateContainer_Click(object sender, EventArgs e)
        {
            if (containersListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um container");
                return;
            }
            string container = containersListBox.SelectedItem.ToString();

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "container");
            doc.AppendChild(root);
            XmlElement containerElement = doc.CreateElement("container");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = updateContainerTextBox.Text;
            containerElement.AppendChild(name);
            root.AppendChild(containerElement);
            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container);

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

                    MessageBox.Show("Nome atualizado com sucesso");
                    loadContainerListBox();

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
    }
}
