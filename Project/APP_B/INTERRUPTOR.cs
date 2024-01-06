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

namespace APP_B
{
    public partial class INTERRUPTOR : Form
    {
        string baseURI = @"http://localhost:59454/api/somiod";
        public INTERRUPTOR()
        {
            InitializeComponent();
        }

        private void btnAbrirGaragem_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement statusElement = doc.CreateElement("status");
            statusElement.InnerText = "abrir";
            doc.AppendChild(statusElement);
            string xmlContent = doc.OuterXml;
            createData("abrir", xmlContent);
        }

        private void btnFecharGaragem_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement statusElement = doc.CreateElement("status");
            statusElement.InnerText = "fechar";
            doc.AppendChild(statusElement);
            string xmlContent = doc.OuterXml;
            createData("fechar", xmlContent);
        }

        public void createData(string valor, string content)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "data");
            doc.AppendChild(root);
            XmlElement data = doc.CreateElement("data");
            XmlElement name = doc.CreateElement("name");
            XmlElement contentElement = doc.CreateElement("content");

            contentElement.InnerXml = content;

            // create a timestamp
            long timeStamp = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            name.InnerText = valor + timeStamp;

            data.AppendChild(contentElement);
            data.AppendChild(name);
            root.AppendChild(data);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Garage/garage_door/");

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
                        if (name.InnerText.Contains("fechar"))
                        {
                            MessageBox.Show("A fechar a garagem");
                        }
                        if (name.InnerText.Contains("abrir"))
                        {
                            MessageBox.Show("A abrir a garagem");
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

        private void APP_B_Load(object sender, EventArgs e)
        {
            createApplication();
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
            name.InnerText = "Switch";
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
                        MessageBox.Show("Interruptor criado com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {

                return;

            }

        }
    }

}
