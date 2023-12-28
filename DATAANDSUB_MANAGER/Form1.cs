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
    }
}
