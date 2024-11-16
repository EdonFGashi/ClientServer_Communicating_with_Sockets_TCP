using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Xml.Linq;
using System.Globalization;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;


namespace Server
{
    public partial class Main : Form
    {
        Listener listener;
        string sharedFolderPath;
        public Main()
        {
            InitializeComponent();

            listClients.FullRowSelect = true;
            listClients.ItemActivate += ListView_ItemActivate;
        }

        // Event handler when a row is clicked in the ListView
        private void ListView_ItemActivate(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (listClients.SelectedItems.Count > 0)
            {
                var selectedItem = listClients.SelectedItems[0];

                // Get the value from column 2 (index 1) and set it in the TextBox
                txtClient.Text = selectedItem.SubItems[1].Text.ToString();


                try
                {
                    //Invoke((MethodInvoker)delegate
                    //{
                    string sharedFolderPath = txtSharedFolderPath.Text;
                    for (int i = 0; i < listClients.Items.Count; i++)
                    {
                        Client client = listClients.Items[i].Tag as Client;

                        if (client.ID == txtClient.Text)
                        {
                            if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute))
                            {
                                checkRead.Checked = true;
                                checkWrite.Checked = false;
                                checkExecute.Checked = false;
                            }
                            else if (!client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                                && client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute))
                            {
                                checkRead.Checked = false;
                                checkWrite.Checked = true;
                                checkExecute.Checked = false;
                            }
                            else if (!client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                                && client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute))
                            {
                                checkRead.Checked = false;
                                checkWrite.Checked = false;
                                checkExecute.Checked = true;
                            }
                            else if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.All))
                            {
                                checkRead.Checked = true;
                                checkWrite.Checked = true;
                                checkExecute.Checked = true;
                            }
                            else if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.ReadWrite))
                            {
                                checkRead.Checked = true;
                                checkWrite.Checked = true;
                                checkExecute.Checked = false;
                            }

                            break;
                        }
                    }
                    //});
                }
                catch (StackOverflowException soe)
                {

                }
            }
        }

        void Main_Load(object sender, EventArgs e)
        {
            //listener.Start();

        }

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += new Client.ClientReceivedHandler(client_Received);
            client.Disconnected += new Client.ClientDisconnectedHandler(client_Disconnected);

            //krijimi i instances qe ruan permission per klientin qe u lidh
            ClientPermissions client1Permissions = new ClientPermissions(client.ID);
            client.setClientPermissions(client1Permissions);

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.SubItems.Add(client.ID);
                i.SubItems.Add("XX");
                i.SubItems.Add("XX");
                i.SubItems.Add(client.clientName.ToString());
                i.Tag = client;
                listClients.Items.Add(i);
            });

        }

        void client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < listClients.Items.Count; i++)
                {
                    Client client = listClients.Items[i].Tag as Client;

                    if (client.ID == sender.ID)
                    {
                        listClients.Items.RemoveAt(i);
                        break;
                    }
                }

            });
        }

        void client_Received(Client sender, byte[] data)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    string clientRequest = Encoding.Default.GetString(data);
                    string clearRequest = "";
                    string filePathRequest = txtSharedFolderPath.Text;


                    for (int i = 0; i < listClients.Items.Count; i++)
                    {
                        Client client = listClients.Items[i].Tag as Client;

                        if (client.ID == sender.ID)
                        {
                            listClients.Items[i].SubItems[2].Text = Encoding.Default.GetString(data);
                            listClients.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                            listBox1.Items.Add(client.clientName + " -- " + Encoding.Default.GetString(data));







                            //nese karakteri i pare esht ~ atehere kemi te bejme me ndonje kerkese per read, write apo execute
                            if (clientRequest.StartsWith("~"))
                            {

                                if (clientRequest.Substring(1, 4) == "read") //kerkesa per read
                                {
                                    listBox1.Items.Add(clientRequest + "  ======  client name: " + client.clientName);
                                    //filePathRequest += clientRequest.Substring(6);

                                    //kontrollojme nese klienti ka leje per kete kerkese
                                    if (client.Permissions.HasPermission(filePathRequest, FilePermissions.Read))
                                    {
                                        //// Client has read permission, send file to client
                                        ///




                                        //////////////////////////////////////
                                        
                                    }
                                    else // nese nuk ka permission atehere i kthejme nje mesazh
                                    {
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for reading file " + filePathRequest + " has refused. Time: " + DateTime.Now);
                                        client.sck.Send(Encoding.Default.GetBytes("SERVER WARNING: You do not have permission to read this file."));
                                    }

                                }
                                else if (clientRequest.Substring(1, 4) == "writ") // kerkesa per write
                                {
                                    listBox1.Items.Add(clientRequest + "  ======  client name: " + client.clientName);

                                    //kontrollojme nese klienti ka leje per kete kerkese
                                    if (client.Permissions.HasPermission(filePathRequest, FilePermissions.Write))
                                    {
                                        //// Client has read permission, send file to client
                                        //SendFileToClient(clientSocket, requestedFile);




                                        ///////////////////////////////////////
                                        
                                    }
                                    else
                                    {
                                        byte[] denyMessage = Encoding.Default.GetBytes("SERVER WARNING: You do not have permission to write in this file.");
                                        client.sck.Send(denyMessage);
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for writing file " + filePathRequest + " has refused. Time: " + DateTime.Now);
                                    }
                                }
                                else if (clientRequest.Substring(1, 4) == "exec") // kerkesa per execute
                                {
                                    listBox1.Items.Add(clientRequest + "  ======  client name: " + client.clientName);

                                    //kontrollojme nese klienti ka leje per kete kerkese
                                    if (client.Permissions.HasPermission(filePathRequest, FilePermissions.Execute))
                                    {
                                        //// Client has read permission, send file to client
                                        //SendFileToClient(clientSocket, requestedFile);
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for executing file " + filePathRequest + " has accepted. Time: " + DateTime.Now);
                                        client.sck.Send(Encoding.Default.GetBytes("SERVER: File is executing..."));

                                        
                                    }
                                    else
                                    {
                                        byte[] denyMessage = Encoding.Default.GetBytes("SERVER WARNING: You do not have permission to execute this file.");
                                        client.sck.Send(denyMessage);
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for executing file " + filePathRequest + " has refused. Time: " + DateTime.Now);
                                    }
                                }
                            }

                            break;
                        }
                    }
                });
            }
            catch (StackOverflowException soe)
            {

            }

        }









        // butoni per te filluar degjimin
        private void button1_Click(object sender, EventArgs e)
        {
            string portText = txtPort.Text;
            int.TryParse(portText, out int port);
            listener = new Listener(port);

            //listener.setPort(port);
            listener.SocketAccepted += new Listener.SocketAcceptedHandler(listener_SocketAccepted);
            Load += new EventHandler(Main_Load);
            listener.Start();

            btnListen.BackColor = Color.LightBlue;
            sharedFolderPath = txtSharedFolderPath.Text.ToString();
        }



        //Metod per menaxhimin e permissions
        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    string sharedFolderPath = txtSharedFolderPath.Text;
                    for (int i = 0; i < listClients.Items.Count; i++)
                    {
                        Client client = listClients.Items[i].Tag as Client;

                        if (client.ID == txtClient.Text)
                        {
                            if (checkRead.Checked && !checkWrite.Checked && !checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.Read);
                            }
                            else if (!checkRead.Checked && checkWrite.Checked && !checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.Write);
                            }
                            else if (!checkRead.Checked && !checkWrite.Checked && checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.Execute);
                            }
                            else if (checkRead.Checked && checkWrite.Checked && !checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.ReadWrite);
                            }
                            else if (checkRead.Checked && checkWrite.Checked && checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.All);
                            }

                            break;
                        }
                    }
                });
            }
            catch (StackOverflowException soe)
            {

            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {

        }
    }
}
