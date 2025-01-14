﻿using APP_A.Properties;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace APP_A
{
    public partial class GARAGEM : Form
    {
        string baseURI = @"http://localhost:59454/api/somiod";
        MqttClient mClient = null;
        public GARAGEM()
        {
            InitializeComponent();
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
            name.InnerText = "Garage";
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
                        MessageBox.Show("Aplicação Garagem criado com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {

                return;
                
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
            name.InnerText = "garage_door";
            container.AppendChild(name);
            root.AppendChild(container);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Garage");

            request.Method = "POST";
            request.ContentType = "application/xml";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
            request.ContentLength = xmlBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(xmlBytes, 0, xmlBytes.Length);
            }
            mClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
            string[] mStrTopicsInfo = { ("Garage" + name.InnerText).ToLower() };
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }; //QoS – depends on the topics number
            mClient.Subscribe(mStrTopicsInfo, qosLevels);
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
                        MessageBox.Show("Container garage_door criado com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {
                
                return;
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
            endpoint.InnerText = "mqtt://127.0.0.1";
            subscription.AppendChild(endpoint);
            XmlElement subEvent = doc.CreateElement("event");
            subEvent.InnerText = "1";
            subscription.AppendChild(subEvent);
            root.AppendChild(subscription);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/Garage/garage_door");

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
                        MessageBox.Show("Subscription sub1 criada com sucesso");
                    }
                }
            }
            catch (WebException ex)
            {
                return;
            }

        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
           
            Image garagemFechada = Resources.garagemFechada;
            Image garagemAberta = Resources.garagemAberta;
            string receivedContent = Encoding.UTF8.GetString(e.Message);

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(receivedContent);
            }
            catch (Exception)
            {

                return;
            }
            XmlNode status = doc.SelectSingleNode("/status");
            if (status.InnerText == "abrir")
            {
                pictureBoxLamp.Image = garagemAberta;
            }
            else if(status.InnerText == "fechar")
            {
                pictureBoxLamp.Image = garagemFechada;
            }
        }

        private void GARAGEM_Load(object sender, EventArgs e)
        {
            createApplication();
            createContainer();
            createSubscription();
        }
    }
}
