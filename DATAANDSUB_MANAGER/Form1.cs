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

namespace DATAANDSUB_MANAGER
{
    public partial class Form1 : Form
    {
        string baseURI = @"http://localhost:59454/api/somiod";
        string application;
        string container;
        public Form1()
        {
            InitializeComponent();
        }

        private void getDataAndSubButton_Click(object sender, EventArgs e)
        {
            if(appTextBox.Text.Length == 0 )
            {
                MessageBox.Show("Insira o nome da aplicação");
                return;
            }
            if(containerTextBox.Text.Length == 0 ) 
            {
                MessageBox.Show("Insira o nome do container");
                return;
            }
            application = appTextBox.Text;
            container = containerTextBox.Text;
            loadDataListBox();
            loadSubListBox();
        }

        private void loadDataListBox()
        {
            dataListBox.Items.Clear();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container);
            request.Method = "GET";
            request.ContentType = "application/xml";
            request.Headers.Add("somiod-discover", "data");

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
                        XmlNodeList datas = doc.SelectNodes("//data");
                        foreach (XmlNode data in datas)
                        {
                            dataListBox.Items.Add(data["name"].InnerText);
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

        private void loadSubListBox()
        {
            subListBox.Items.Clear();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container);
            request.Method = "GET";
            request.ContentType = "application/xml";
            request.Headers.Add("somiod-discover", "subscription");

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
                        XmlNodeList subs = doc.SelectNodes("//subscription");
                        foreach (XmlNode subscription in subs)
                        {
                            subListBox.Items.Add(subscription["name"].InnerText);
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

        private void deleteDataButton_Click(object sender, EventArgs e)
        {
            if (dataListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma data");
                return;
            }

            string data = dataListBox.SelectedItem.ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container + "/data/" + data);

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
                        loadDataListBox();
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

        private void deleteSubscriptionButton_Click(object sender, EventArgs e)
        {
            if (subListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma subscricao");
                return;
            }

            string sub = subListBox.SelectedItem.ToString();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container + "/sub/" + sub);

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
                        loadSubListBox();
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

        private void createDataButton_Click(object sender, EventArgs e)
        {
            if(application.Length == 0)
            {
                MessageBox.Show("Insira o nome da aplicacao");
                return;
            }
            if (container.Length == 0)
            {
                MessageBox.Show("Insira o nome do container");
                return;
            }
            if (dataNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o nome da data");
                return;
            }
            if(dataContentTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o content da data");
                return;
            }


            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "data");
            doc.AppendChild(root);
            XmlElement data = doc.CreateElement("data");
            XmlElement name = doc.CreateElement("name");
            XmlElement contentElement = doc.CreateElement("content");

            contentElement.InnerText = dataContentTextBox.Text;
            name.InnerText = dataNameTextBox.Text;

            data.AppendChild(contentElement);
            data.AppendChild(name);
            root.AppendChild(data);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI +"/"+ application + "/" + container);

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
                        loadDataListBox();
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

        private void createSubButton_Click(object sender, EventArgs e)
        {
            if (application.Length == 0)
            {
                MessageBox.Show("Insira o nome da aplicacao");
                return;
            }
            if (container.Length == 0)
            {
                MessageBox.Show("Insira o nome do container");
                return;
            }
            if (subNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o nome da subscricao");
                return;
            }
            if (subEventTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o event da subscricao");
                return;
            }
            if (subEndpointTextBox.Text.Length == 0)
            {
                MessageBox.Show("Insira o endpoint da subscricao");
                return;
            }


            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("res_type", "subscription");
            doc.AppendChild(root);
            XmlElement subscription = doc.CreateElement("subscription");
            XmlElement name = doc.CreateElement("name");
            name.InnerText = subNameTextBox.Text;
            subscription.AppendChild(name);
            XmlElement endpoint = doc.CreateElement("endpoint");
            endpoint.InnerText = subEndpointTextBox.Text;
            subscription.AppendChild(endpoint);
            XmlElement subEvent = doc.CreateElement("event");
            subEvent.InnerText = subEventTextBox.Text;
            subscription.AppendChild(subEvent);
            root.AppendChild(subscription);

            string xmlContent = doc.OuterXml;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI + "/" + application + "/" + container);

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
                        loadSubListBox();
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
    }
}
