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
using uPLibrary.Networking.M2Mqtt.Messages;

namespace APP_A
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:59454/api/somiod";
        MqttClient mClient = null;
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

            mClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
            string[] mStrTopicsInfo = { name.InnerText };
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }; //QoS – depends on the topics number
            mClient.Subscribe(mStrTopicsInfo, qosLevels);
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
            endpoint.InnerText = "mqtt://127.0.0.1";
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

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Image lampadaDesligada = Image.FromFile(@"lampadaDesligada.png");
            Image lampadaLigada = Image.FromFile(@"lampadaLigada.png"); 
            string receivedContent = Encoding.UTF8.GetString(e.Message);

            if(receivedContent == "1")
            {
                pictureBoxLamp.Image = lampadaLigada;
            }
            else if(receivedContent == "0")
            {
                pictureBoxLamp.Image = lampadaDesligada;
            }
        }

        private void deleteApp_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting");

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
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }

        private void deleteContainer_Click(object sender, EventArgs e)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting/light_bulb");

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
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }

        private void DeleteSubscription_Click(object sender, EventArgs e)
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
            root.AppendChild(subscription);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting/light_bulb/sub/" + name.InnerText);

            request.Method = "DELETE";
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
                        MessageBox.Show("Eliminado com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }

        private void Delete_Data_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "data");
            doc.AppendChild(root);
            XmlElement data = doc.CreateElement("data");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "on";
            data.AppendChild(name);
            root.AppendChild(data);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting/light_bulb/data/" + name.InnerText);

            request.Method = "DELETE";
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
                        MessageBox.Show("Eliminado com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show($"WebException: {ex.Message}");
            }
        }

        private void buttonUpdateAppName_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "application");
            doc.AppendChild(root);
            XmlElement application = doc.CreateElement("application");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "UPDATEEE!!";
            application.AppendChild(name);
            root.AppendChild(application);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Lighting");

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

                    MessageBox.Show("Nome atualizado com sucesso com sucesso");

                }
            }
            catch (WebException ex)
            {
                // Check if the exception has a response
                if (ex.Response != null)
                {
                    // Get the response from the exception
                    using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                    {
                        // Read the error message from the response
                        using (Stream errorStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(errorStream);
                            string errorMessage = reader.ReadToEnd();

                            MessageBox.Show($"WebException: {ex.Message}\nError Message: {errorMessage}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"WebException: {ex.Message}");
                }
            }
        }

        private void buttonUpdateContainerName_Click(object sender, EventArgs e)
        {

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "container");
            doc.AppendChild(root);
            XmlElement application = doc.CreateElement("container");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = "novo_nome_container";
            application.AppendChild(name);
            root.AppendChild(application);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/LIGHT/light_bulb");

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

                    MessageBox.Show("Nome atualizado com sucesso com sucesso");

                }
            }
            catch (WebException ex)
            {
                // Check if the exception has a response
                if (ex.Response != null)
                {
                    // Get the response from the exception
                    using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                    {
                        // Read the error message from the response
                        using (Stream errorStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(errorStream);
                            string errorMessage = reader.ReadToEnd();

                            MessageBox.Show($"WebException: {ex.Message}\nError Message: {errorMessage}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"WebException: {ex.Message}");
                }
            }
        }
    }
}
