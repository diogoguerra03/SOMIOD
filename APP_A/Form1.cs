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

namespace APP_A
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
            createApplication();
            createContainer();
            createSubscription();
        }

        public void createApplication()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "application");
            doc.AppendChild(root);
            XmlElement application = doc.CreateElement("application");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "Lighting";
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
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }

        public void createContainer()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "container");
            doc.AppendChild(root);
            XmlElement container = doc.CreateElement("container");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "light_bulb";
            container.AppendChild(name);
            root.AppendChild(container);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting");

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
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }

        public void createSubscription()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "subscription");
            doc.AppendChild(root);
            XmlElement subscription = doc.CreateElement("subscription");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "sub1";
            subscription.AppendChild(name);
            XmlElement endpoint = doc.CreateElement("endpoint");
            endpoint.InnerText = "mqtt://192.168.1.2";
            subscription.AppendChild(endpoint);
            root.AppendChild(subscription);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting/light_bulb");

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
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }
    }
}
